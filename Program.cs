using System;
using System.Reflection.PortableExecutable;
namespace RayTracing
{

    class Program
    {

        class Sphere
        {
            public Vector centre;
            public double radious;
            public Sphere(Vector p, double r)
            {
                centre = p;
                radious = r;
            }

        }
        class Plane
        {
            // Vector v1;
            // Vector v2;
            // Vector v3;

            //normal
            Vector point;
            Vector normal;

            // public Plane(Vector p1, Vector p2, Vector p3)
            // {
            //     v1 = p1;
            //     v2 = p2;
            //     v3 = p3;
            // }
            public Plane(Vector p1, Vector n)
            {
                point = p1;
                normal = n;
                //Calculate points
            }
        }
        class Ray
        {
            Vector start;
            Vector dir;
            public Ray(double x, double y, double z, Vector dir)
            {
                start = new Vector(x, y, z);
                this.dir = dir;
            }
            public Ray(Vector start, Vector dir)
            {
                this.start = start;
                this.dir = dir;
            }
            public bool intersect(Sphere s) //something wonk
            {
                Vector oc = start.sub(s.centre);
                double a = dir.dot(dir, dir);
                double b = oc.dot(oc, dir);
                double c = oc.dot(oc, oc) - s.radious * s.radious;

                double discriminant = b * b - 4 * a * c;

                if (discriminant < 0) return false;
                if (discriminant == 0) return true;

                //double temp = -b - Math.Sqrt(discriminant) / a;

                return true;
            }
            public bool intersect(Triangle t)
            {
                return false;
            }
            public bool intersect(Plane t)
            {
                return false;
            }
        }

        class Triangle
        {
            Vector a;
            Vector b;
            Vector c;


            public Triangle(Vector point1, Vector point2, Vector point3)
            {
                a = point1;
                b = point2;
                c = point3;

            }
        }



        static void Main(string[] args)
        {
            Console.WriteLine("Testing Basics Vector methods");
            Vector a = new Vector(1.0, 0, 0);
            Vector b = new Vector(0, 0, 1.0);
            a.show();
            b.show();
            Console.WriteLine("A plus B");
            a.add(b);
            a.show();
            Console.WriteLine("B plus A");
            Vector a1 = new Vector(1.0, 0, 0);
            b.add(a1);
            b.show();
            Console.WriteLine("Angle between A and B");
            a = new Vector(0, 3, 0);
            b = new Vector(5, 5, 0);
            double angle = a.angle(a, b);
            Console.WriteLine($"Angle: {angle} deg");
            //Znajdź wektor prostopadły do wektorów [4,5,1] i [4,1,3] 
            Console.WriteLine("Vector orthogonal to A & B");
            a = new Vector(4, 5, 1);
            b = new Vector(4, 1, 3);
            Vector c = a.cross(a, b);
            c.show();
            c.normalize();
            c.show();

            Console.WriteLine("Sphere");
            Sphere sphere = new Sphere(new Vector(0, 0, 0), 10);
            Vector start = new Vector(0, 0, -20);
            Ray ray1 = new Ray(start, sphere.centre);

            Ray ray2 = new Ray(start, new Vector(0, 1, -20));
            //10. przecięcie sfery z promieniami
            Console.WriteLine($"Sphere and ray1: {ray1.intersect(sphere)}");
            Console.WriteLine($"Sphere and ray2: {ray2.intersect(sphere)}");
            //11 
            Console.WriteLine("Intersection Coordinates: {}");
            //12
            Ray ray3 = new Ray(new Vector(0, 10, 0), new Vector(1, 10, 0));
            Console.WriteLine("Intersection Coordinates: {10,10,0}");
            //13
            Plane plane = new Plane(new Vector(0, 0, 0), new Vector(0, 1, 1));
            //14 todo
            Console.WriteLine("R2 and plane 1 Intersection Coordinates: {}");
            //15
            Triangle t1 = new Triangle(new Vector(0, 0, 0), new Vector(1, 0, 0), new Vector(0, 1, 0));

            Ray triangleRay = new Ray(new Vector(-1, 0.5, 0), new Vector(1, 0.5, 0));
            Console.WriteLine($"Triangle and ray1: {triangleRay.intersect(t1)}");
            Ray triangleRay1 = new Ray(new Vector(2, -1, 0), new Vector(2, 2, 0));
            Console.WriteLine($"Triangle and ray2: {triangleRay1.intersect(t1)}");
            Ray triangleRay2 = new Ray(new Vector(0, 0, -1), new Vector(0, 0, 1));
            Console.WriteLine($"Triangle and ray3: {triangleRay2.intersect(t1)}");

        }
    }
    //Każda klasa - 1 plik

}
