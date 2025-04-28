using System.ComponentModel;
using System.Drawing;

namespace RayTracing;

public class OrthogonalCamera : Camera
{

    Image image;
    public OrthogonalCamera(Vector v, Image i)
    {

        image = i;
    }
    public override void See()
    {
        Sphere sphere = new Sphere(new Vector(50, 150, 0), 30);
        Sphere sphere2 = new Sphere(new Vector(200, 300, 0), 100);
        LightIntensity color = new LightIntensity(100, 0, 0);
        LightIntensity color3 = new LightIntensity(0, 0, 150);
        LightIntensity bgColor = new LightIntensity(0, 10, 10);

        float pixelW = 2.0f / image.Width;
        float pixelH = 2.0f / image.Height;

        for (int i = 0; i < image.Width; i++)
        {
            for (int j = 0; j < image.Height; j++)
            {
                float centreX = -1.0f + (i + 0.5f) * pixelW;
                float centreY = 1.0f - (j + 0.5f) * pixelH;
                Ray ray = new Ray(new Vector(i + centreX, j + centreY, 0), new Vector(0, 0, 1));


                IntersectionInfo intersectionInfo = sphere.Intersect(ray);
                if (intersectionInfo.doesIntersect())
                {
                    image.setPixel(i, j, color);
                }
                else
                {
                    image.setPixel(i, j, bgColor);
                }
                IntersectionInfo intersectionInfo2 = sphere.Intersect(ray);
                if (intersectionInfo2.doesIntersect())
                {
                    image.setPixel(i, j, color3);

                }

            }
        }
        image.saveImage("Camera.png");
    }
}
