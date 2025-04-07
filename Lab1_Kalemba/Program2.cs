using System.Drawing;
using System.Windows;
using System.Security.AccessControl;
using SkiaSharp;
using System.Xml;

namespace RayTracing;

public class Program2
{

    static void Main(string[] args)
    {
        Console.WriteLine("Define Image");
        Image image = new(600, 600);
        // LightIntensity pixel = new LightIntensity(10, 0, 1);
        // image.drawSquare(pixel);
        // image.saveImage("output.png");
        // Console.WriteLine("Image saved");
        //OrthogonalCamera camera = new OrthogonalCamera(new Vector(0, 0, 0), image);
        //camera.See();
        Console.WriteLine("Camera Saw something");

        PerspectiveCamera camera1 = new PerspectiveCamera(image);
        camera1.See();
        camera1.see_aa_regular();
        camera1.see_aa_random();
    }


}





