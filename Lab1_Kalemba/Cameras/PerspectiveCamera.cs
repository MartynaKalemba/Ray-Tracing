using System.Runtime.Intrinsics;

namespace RayTracing;

public class PerspectiveCamera : Camera
{
    private Image image;

    private Scene scene;

    public PerspectiveCamera(Image i)
    {
        image = i;

        // Precompute scene objects and colors
        Sphere[] spheres =
        {
        new Sphere(new Vector(0.5f, 3, 4), 2),
        new Sphere(new Vector(-0.5f, -0.5, 2), 1)
    };

        Plane[] planes =
        {
            new Plane(new Vector(0,0,8), new Vector(0,0,1)),
            new Plane(new Vector(-4,0,0), new Vector(1,0,0)),
            new Plane(new Vector(4,0,0), new Vector(-1,0,0))
        };

        LightIntensity[] colors =
        {
        new LightIntensity(0.5, 0.1, 0.1),   // Red for sphere 1
        new LightIntensity(0.1, 0.1, 1),
        new LightIntensity(0.1, 0.1, 0.1)   // Blue for sphere 2
    };

        Material[] materials =
        { new Material(colors[0],1,10,1),
         new Material(colors[1],1,100,1f),
         new Material(colors[2],1,1,1)
    };

        List<Primitive> primitives = [
        new Primitive(spheres[0], materials[0]),
         new Primitive(spheres[1], materials[1]),
         new Primitive(planes[0], materials[2]),
         new Primitive(planes[1], materials[0]),
         new Primitive(planes[2], materials[1])
         ];


        Vector dirLight = Vector.Sub(new Vector(0, 0, 0), spheres[0].centre);
        dirLight.normalize();
        Plane p = new Plane(new Vector(0, 0, 0), dirLight);
        Plane b = new Plane(new Vector(1, 1, 0), dirLight);
        DirectionalLight light = new DirectionalLight(p, new LightIntensity(0.5, 0.5, 0.5));
        DirectionalLight lightb = new DirectionalLight(b, new LightIntensity(0.1, 0.1, 0.1));
        DirectionalLight lightc = new DirectionalLight(p, new LightIntensity(0.1, 0.1, 0.1));
        AmbientLight al = new AmbientLight(new LightIntensity(0.1, 0.1, 0.1));
        List<Light> lights = [light];
        LightIntensity bgColor = new LightIntensity(255, 255, 255);
        scene = new Scene(primitives, lights, al, bgColor);
    }


    public override void See()
    {

        LightIntensity bgColor = scene.BgColor!;
        List<Primitive> primitives = scene.Primitives;
        List<Light> lights = scene.Lights;

        float pixelW = 2.0f / image.Width;
        float pixelH = 2.0f / image.Height;
        Vector rayStart = new Vector(0, 0, 0);

        // Parallelize the outer loop for multi-core CPUs
        Parallel.For(0, image.Width, i =>
        {
            for (int j = 0; j < image.Height; j++)
            {
                // Calculate ray direction
                float x = -1.0f + (i + 0.5f) * pixelW;
                float y = 1.0f - (j + 0.5f) * pixelH;

                Vector dirVector = new Vector(x, y, 1);
                dirVector.normalize();
                Ray ray = new Ray(rayStart, dirVector);

                LightIntensity pixelColor = bgColor;
                double closestT = double.MaxValue;

                // Check all primitives
                for (int k = 0; k < primitives.Count; k++)
                {
                    IntersectionInfo intersectionInfo = primitives[k].GetShape().Intersect(ray);
                    if (intersectionInfo.doesIntersect())
                    {


                        double t;
                        Vector closerPoint = intersectionInfo.getClosestIntersection(rayStart, out t);
                        Vector normal = primitives[k].GetShape().getNormal(closerPoint);
                        //calculate lights
                        LightIntensity addedLight = getLight(closerPoint, dirVector, lights, primitives, primitives[k].GetMaterial(), normal);
                        if (t < closestT)
                        {
                            closestT = t;
                            pixelColor = addedLight;

                        }
                    }

                }

                image.setPixel(i, j, pixelColor);
            }
        });
        image.saveImage("PCameraa.png");
    }

    //todo 
    //Wprowadzić światło punktowe
    //Naprawić kolory
    //Popracować nad rekursywnością
    private LightIntensity getLight(Vector point, Vector cameraDir, List<Light> lights, List<Primitive> primitives, Material material, Vector normal)
    {
        LightIntensity lightAdded = new LightIntensity(0, 0, 0);
        foreach (DirectionalLight l in lights)
        {

            Vector PQ = Vector.Sub(point, l.plane.point);
            double distance = Vector.Dot(PQ, l.plane.normal);
            Vector projection = Vector.Sub(point, Vector.Mag(l.plane.normal, distance));

            Vector lightDirection = Vector.Sub(point, projection);
            lightDirection.normalize();

            //Calculating the light direction, sending ray to LIGHT

            Ray ray = new Ray(point, lightDirection);


            //Direction = I
            //normal = N  
            // R 
            Vector r = Vector.Sub(lightDirection, Vector.Mag(normal, lightDirection.length() * lightDirection.length()));
            r.normalize();
            double cosTheta = Vector.Dot(r, cameraDir);
            double cosAlpha = Vector.Dot(lightDirection, normal);
            double cosAngle = Vector.Dot(lightDirection, cameraDir); /// (direction.length() * dir.length());
            if (cosAngle < 0) cosAngle = 0;
            double sinAngle = Math.Sqrt(1 - cosAngle * cosAngle);

            bool lightHidden = false;
            foreach (Primitive p in primitives)
            {
                lightHidden = false;
                IntersectionInfo intersectionInfo = p.GetShape().Intersect(ray);
                if (intersectionInfo.doesIntersect())
                {
                    lightAdded *= new LightIntensity(0.1, 0.1, 0.1);
                    lightAdded += scene.AmbientLight!.I + material.diffuse_colour * l.I * cosAngle * material.reflect_fraction +
                     l.I * Math.Pow(cosTheta, material.specular_coeff) * material.specular_amount;
                    lightHidden = true;
                    break;
                }
                lightAdded -= scene.AmbientLight!.I + material.diffuse_colour * l.I * cosAngle * material.reflect_fraction +
                     l.I * Math.Pow(cosTheta, material.specular_coeff) * material.specular_amount;
            }
            if (lightHidden) continue;
            lightAdded -= material.diffuse_colour * l.I * cosAngle * material.specular_coeff +
                    material.diffuse_colour * l.I * cosTheta * material.specular_amount;


        }

        return lightAdded;
    }

    private LightIntensity antiAliasing()
    {
        return new LightIntensity();
    }


}



// public void see_aa_regular()
// {
//     // Scene setup
//     Sphere[] spheres =
//     {
//     new Sphere(new Vector(0.5f, 0, 6), 2),
//     new Sphere(new Vector(-0.5f, 1, 2), 1)
// };

//     LightIntensity[] colors =
//     {
//     new LightIntensity(100, 0, 0),
//     new LightIntensity(0, 0, 150)
// };

//     LightIntensity bgColor = new LightIntensity(0, 10, 10);

//     // Anti-aliasing settings (4 samples per pixel)
//     const int aaSamples = 2; // 2x2 grid
//     float aaStep = 1.0f / aaSamples;
//     float aaOffset = aaStep * 0.5f;

//     // Precompute pixel dimensions
//     float pixelW = 2.0f / image.Width;
//     float pixelH = 2.0f / image.Height;
//     Vector rayStart = new Vector(0, 0, 0);

//     Parallel.For(0, image.Width, i =>
//     {
//         for (int j = 0; j < image.Height; j++)
//         {
//             LightIntensity accumulatedColor = new LightIntensity(0, 0, 0);
//             bgColor.set(i % 255, i % 255, j % 255);
//             // Super-sampling loop
//             for (float aaX = 0; aaX < aaSamples; aaX++)
//             {
//                 for (float aaY = 0; aaY < aaSamples; aaY++)
//                 {
//                     // Calculate jittered sample position within pixel
//                     float sampleX = i + aaX * aaStep + aaOffset;
//                     float sampleY = j + aaY * aaStep + aaOffset;

//                     // Calculate ray direction
//                     float x = -1.0f + sampleX * pixelW;
//                     float y = 1.0f - sampleY * pixelH;

//                     Vector dirVector = new Vector(x, y, 1);
//                     dirVector.normalize();
//                     Ray ray = new Ray(rayStart, dirVector);

//                     double closestT = double.MaxValue;
//                     LightIntensity sampleColor = new LightIntensity(bgColor.R, bgColor.G, bgColor.B);

//                     // Check all spheres
//                     for (int k = 0; k < spheres.Length; k++)
//                     {
//                         if (ray.intersect(spheres[k], out Vector intersection, out Vector intersection1))
//                         {
//                             double d1 = Vector.sub(intersection, rayStart).length();
//                             double d2 = Vector.sub(intersection1, rayStart).length();
//                             double t = Math.Min(d1, d2);

//                             if (t < closestT)
//                             {
//                                 closestT = t;
//                                 sampleColor = colors[k];
//                             }
//                         }
//                     }

//                     accumulatedColor.Add(sampleColor);
//                 }
//             }

//             // Average the samples
//             accumulatedColor.Divide(aaSamples * aaSamples);
//             image.setPixel(i, j, accumulatedColor);
//         }
//     });

//     image.saveImage("PCamera2_AA.png");
// }

// public void see_aa_random()
// {
//     // Scene setup
//     Sphere[] spheres =
//     {
//     new Sphere(new Vector(0.5f, 0, 6), 2),
//     new Sphere(new Vector(-0.5f, 1, 2), 1)
// };

//     LightIntensity[] colors =
//     {
//     new LightIntensity(100, 0, 0),
//     new LightIntensity(0, 0, 150)
// };

//     LightIntensity bgColor = new LightIntensity(0, 10, 10);

//     // Anti-aliasing settings (4 samples per pixel)
//     const int aaSamples = 2; // 2x2 grid
//     float aaStep = 1.0f / aaSamples;
//     float aaOffset = aaStep * 0.5f;

//     // Precompute pixel dimensions
//     float pixelW = 2.0f / image.Width;
//     float pixelH = 2.0f / image.Height;
//     Vector rayStart = new Vector(0, 0, 0);

//     Parallel.For(0, image.Width, i =>
//     {
//         for (int j = 0; j < image.Height; j++)
//         {
//             LightIntensity accumulatedColor = new LightIntensity(0, 0, 0);
//             bgColor.set(i % 255, i % 255, j % 255);


//             // Calculate jittered sample position within pixel
//             // Use random jitter instead of grid for better results with fewer samples
//             Random r = new Random();
//             float sampleX = (float)(i + r.NextDouble());
//             float sampleY = (float)(j + r.NextDouble());

//             // Calculate ray direction
//             float x = -1.0f + sampleX * pixelW;
//             float y = 1.0f - sampleY * pixelH;

//             Vector dirVector = new Vector(x, y, 1);
//             dirVector.normalize();
//             Ray ray = new Ray(rayStart, dirVector);

//             double closestT = double.MaxValue;
//             LightIntensity sampleColor = new LightIntensity(bgColor);

//             // Check all spheres
//             for (int k = 0; k < spheres.Length; k++)
//             {
//                 if (ray.intersect(spheres[k], out Vector intersection, out Vector intersection1))
//                 {
//                     //Check if it hit the light
//                     double d1 = Vector.sub(intersection, rayStart).length();
//                     double d2 = Vector.sub(intersection1, rayStart).length();
//                     double t = Math.Min(d1, d2);

//                     if (t < closestT)
//                     {
//                         closestT = t;
//                         sampleColor = colors[k];
//                     }
//                 }
//             }

//             accumulatedColor += sampleColor;



//             // Average the samples
//             accumulatedColor /= aaSamples * aaSamples;
//             image.setPixel(i, j, accumulatedColor);
//         }
//     });

//     image.saveImage("PCamera2_AAR.png");
// }


