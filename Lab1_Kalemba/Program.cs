namespace RayTracing
{
    //Martyna Kalemba 235887

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing Basics Vector methods");
            Vector a = new Vector(1, 0, 0);
            Vector b = new Vector(0, 0, 1);
            a.show();
            b.show();
            Console.WriteLine("A plus B");
            a.add(b);
            a.show();
            Console.WriteLine("B plus A");
            Vector a1 = new Vector(1, 0, 0);
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
            Vector c = Vector.cross(a, b);
            c.show();
            c.normalize();
            c.show();

            Console.WriteLine("Sphere : ");
            Sphere sphere = new Sphere(new Vector(0, 0, 0), 10);
            sphere.show();
            Vector start = new Vector(0, 0, -20);
            Vector dirVector = Vector.Sub(sphere.centre, start);
            dirVector.normalize();
            Ray ray1 = new Ray(start, dirVector);

            Ray ray2 = new Ray(start, new Vector(0, 1, 0));

            Vector v1;
            Vector v2;
            //10. przecięcie sfery z promieniami
            IntersectionInfo intersectionInfo = sphere.Intersect(ray1);
            Console.WriteLine($"Sphere and ray1: {sphere.Intersect(ray1)}");
            //11 wyswietlenie koordynatow
            Console.WriteLine($"Intersection Sphere and ray1:");
            intersectionInfo.show();
            intersectionInfo = sphere.Intersect(ray2);
            Console.WriteLine($"Sphere and ray2: {sphere.Intersect(ray2)}");
            intersectionInfo.show();

            //12
            Ray ray3 = new Ray(new Vector(10, -10, 0), new Vector(-1, 0, 0));
            intersectionInfo = sphere.Intersect(ray3);
            Console.WriteLine("Intersection Coordinates: ");
            intersectionInfo.show();
            //13
            Plane plane = new Plane(new Vector(0, 0, 0), new Vector(0, 1, 1));
            //14
            Console.WriteLine("R2 and plane 1 Intersection Coordinates: {}");
            intersectionInfo = plane.Intersect(ray2);
            intersectionInfo.show();

            //15
            Triangle t1 = new Triangle(new Vector(0, 0, 0), new Vector(1, 0, 0), new Vector(0, 1, 0));

            Ray triangleRay = new Ray(new Vector(-1, (float)0.5, 0), new Vector(1, (float)0.5, 0));
            Console.WriteLine($"Triangle and ray1: {t1.Intersect(triangleRay)}");
            Ray triangleRay1 = new Ray(new Vector(2, -1, 0), new Vector(2, 2, 0));
            Console.WriteLine($"Triangle and ray2: {t1.Intersect(triangleRay1)}");
            Ray triangleRay2 = new Ray(new Vector(0, 0, -1), new Vector(0, 0, 1));
            Console.WriteLine($"Triangle and ray3: {t1.Intersect(triangleRay2)}");

        }
    }

}
