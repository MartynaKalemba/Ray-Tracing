using System.ComponentModel;
using System.Drawing;

namespace RayTracing;

public class OrthogonalCamera
{
    Vector position;
    Image image;
    public OrthogonalCamera(Vector v, Image i)
    {
        position = v;
        image = i;
    }
    public void see()
    {
        Sphere sphere = new Sphere(new Vector(50, 50, 0), 20);
        Sphere sphere2 = new Sphere(new Vector(20, 30, 0), 10);
        LightIntensity color = new LightIntensity(100, 0, 0);
        LightIntensity color3 = new LightIntensity(0, 0, 150);
        LightIntensity bgColor = new LightIntensity(0, 10, 10);

        float pixelW = 2.0f / image.Width;
        float pixelH = 2.0f / image.Height;

        for (int i = 0; i < image.Width; i++)
        {
            for (int j = 0; j < image.Height; j++)
            {
                double centreX = -1.0f + (i + 0.5f) * pixelW;
                double centreY = 1.0f - (j + 0.5f) * pixelH;
                // Ray ray = new Ray(new Vector(centreX, centreY, 4), new Vector(0, 0, 1));
                Ray ray = new Ray(new Vector(i, j, 5), new Vector(0, 0, 1));
                Ray ray1 = new Ray(new Vector(i, j, 0), new Vector(0, 0, 1));
                Vector intersection;
                Vector intersection1;
                if (ray.intersect(sphere, out intersection, out intersection1))
                {
                    image.setPixel(i, j, color);
                    intersection.show();
                    Console.WriteLine("hit");
                }
                else
                {
                    image.setPixel(i, j, bgColor);
                    Console.WriteLine("nohit");
                }
                if (ray1.intersect(sphere2, out intersection, out intersection1))
                {
                    image.setPixel(i, j, color3);
                    intersection.show();
                    Console.WriteLine("anotheeer");
                }

            }
        }
        image.saveImage("Camera.png");
    }
}
