namespace RayTracing;

public class PerspectiveCamera : Camera
{
    private Image image;

    public PerspectiveCamera(Image i)
    {
        image = i;
    }


    public override void See()
    {
        // Precompute scene objects and colors
        Sphere[] spheres =
        {
        new Sphere(new Vector(0.5f, 0, 6), 2),
        new Sphere(new Vector(-0.5f, 1, 2), 1)
    };

        LightIntensity[] colors =
        {
        new LightIntensity(100, 0, 0),   // Red for sphere 1
        new LightIntensity(0, 0, 150)   // Blue for sphere 2
    };

        LightIntensity bgColor = new LightIntensity(0, 10, 10);

        // Precompute pixel dimensions once
        float pixelW = 2.0f / image.Width;
        float pixelH = 2.0f / image.Height;
        Vector rayStart = new Vector(0, 0, 0);

        // Parallelize the outer loop for multi-core CPUs
        Parallel.For(0, image.Width, i =>
        {
            for (int j = 0; j < image.Height; j++)
            {
                bgColor = new LightIntensity(i, i, j);
                // Calculate ray direction
                float x = -1.0f + (i + 0.5f) * pixelW;
                float y = 1.0f - (j + 0.5f) * pixelH;

                Vector dirVector = new Vector(x, y, 1);
                dirVector.normalize();
                Ray ray = new Ray(rayStart, dirVector);

                LightIntensity pixelColor = bgColor;
                double closestT = double.MaxValue;

                // Check all spheres
                for (int k = 0; k < spheres.Length; k++)
                {
                    if (ray.intersect(spheres[k], out Vector intersection, out Vector intersection1))
                    {
                        // Calculate distances more efficiently
                        double d1 = Vector.sub(intersection, rayStart).length();
                        double d2 = Vector.sub(intersection1, rayStart).length();
                        double t = Math.Min(d1, d2);

                        if (t < closestT)
                        {
                            closestT = t;
                            pixelColor = colors[k];
                        }
                    }
                }

                image.setPixel(i, j, pixelColor);
            }
        });

        image.saveImage("PCamera2.png");
    }


    public void see_aa_regular()
    {
        // Scene setup
        Sphere[] spheres =
        {
        new Sphere(new Vector(0.5f, 0, 6), 2),
        new Sphere(new Vector(-0.5f, 1, 2), 1)
    };

        LightIntensity[] colors =
        {
        new LightIntensity(100, 0, 0),
        new LightIntensity(0, 0, 150)
    };

        LightIntensity bgColor = new LightIntensity(0, 10, 10);

        // Anti-aliasing settings (4 samples per pixel)
        const int aaSamples = 2; // 2x2 grid
        float aaStep = 1.0f / aaSamples;
        float aaOffset = aaStep * 0.5f;

        // Precompute pixel dimensions
        float pixelW = 2.0f / image.Width;
        float pixelH = 2.0f / image.Height;
        Vector rayStart = new Vector(0, 0, 0);

        Parallel.For(0, image.Width, i =>
        {
            for (int j = 0; j < image.Height; j++)
            {
                LightIntensity accumulatedColor = new LightIntensity(0, 0, 0);
                bgColor.set(i % 255, i % 255, j % 255);
                // Super-sampling loop
                for (float aaX = 0; aaX < aaSamples; aaX++)
                {
                    for (float aaY = 0; aaY < aaSamples; aaY++)
                    {
                        // Calculate jittered sample position within pixel
                        float sampleX = i + aaX * aaStep + aaOffset;
                        float sampleY = j + aaY * aaStep + aaOffset;

                        // Calculate ray direction
                        float x = -1.0f + sampleX * pixelW;
                        float y = 1.0f - sampleY * pixelH;

                        Vector dirVector = new Vector(x, y, 1);
                        dirVector.normalize();
                        Ray ray = new Ray(rayStart, dirVector);

                        double closestT = double.MaxValue;
                        LightIntensity sampleColor = new LightIntensity(bgColor.R, bgColor.G, bgColor.B);

                        // Check all spheres
                        for (int k = 0; k < spheres.Length; k++)
                        {
                            if (ray.intersect(spheres[k], out Vector intersection, out Vector intersection1))
                            {
                                double d1 = Vector.sub(intersection, rayStart).length();
                                double d2 = Vector.sub(intersection1, rayStart).length();
                                double t = Math.Min(d1, d2);

                                if (t < closestT)
                                {
                                    closestT = t;
                                    sampleColor = colors[k];
                                }
                            }
                        }

                        accumulatedColor.Add(sampleColor);
                    }
                }

                // Average the samples
                accumulatedColor.Divide(aaSamples * aaSamples);
                image.setPixel(i, j, accumulatedColor);
            }
        });

        image.saveImage("PCamera2_AA.png");
    }

    public void see_aa_random()
    {
        // Scene setup
        Sphere[] spheres =
        {
        new Sphere(new Vector(0.5f, 0, 6), 2),
        new Sphere(new Vector(-0.5f, 1, 2), 1)
    };

        LightIntensity[] colors =
        {
        new LightIntensity(100, 0, 0),
        new LightIntensity(0, 0, 150)
    };

        LightIntensity bgColor = new LightIntensity(0, 10, 10);

        // Anti-aliasing settings (4 samples per pixel)
        const int aaSamples = 2; // 2x2 grid
        float aaStep = 1.0f / aaSamples;
        float aaOffset = aaStep * 0.5f;

        // Precompute pixel dimensions
        float pixelW = 2.0f / image.Width;
        float pixelH = 2.0f / image.Height;
        Vector rayStart = new Vector(0, 0, 0);

        Parallel.For(0, image.Width, i =>
        {
            for (int j = 0; j < image.Height; j++)
            {
                LightIntensity accumulatedColor = new LightIntensity(0, 0, 0);
                bgColor.set(i % 255, i % 255, j % 255);


                // Calculate jittered sample position within pixel
                // Use random jitter instead of grid for better results with fewer samples
                Random r = new Random();
                float sampleX = (float)(i + r.NextDouble());
                float sampleY = (float)(j + r.NextDouble());

                // Calculate ray direction
                float x = -1.0f + sampleX * pixelW;
                float y = 1.0f - sampleY * pixelH;

                Vector dirVector = new Vector(x, y, 1);
                dirVector.normalize();
                Ray ray = new Ray(rayStart, dirVector);

                double closestT = double.MaxValue;
                LightIntensity sampleColor = new LightIntensity(bgColor);

                // Check all spheres
                for (int k = 0; k < spheres.Length; k++)
                {
                    if (ray.intersect(spheres[k], out Vector intersection, out Vector intersection1))
                    {
                        double d1 = Vector.sub(intersection, rayStart).length();
                        double d2 = Vector.sub(intersection1, rayStart).length();
                        double t = Math.Min(d1, d2);

                        if (t < closestT)
                        {
                            closestT = t;
                            sampleColor = colors[k];
                        }
                    }
                }

                accumulatedColor += sampleColor;



                // Average the samples
                accumulatedColor /= aaSamples * aaSamples;
                image.setPixel(i, j, accumulatedColor);
            }
        });

        image.saveImage("PCamera2_AAR.png");
    }


}
