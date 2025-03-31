namespace RayTracing
{
    //Martyna Kalemba 235887

    class Program
    {
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
            Vector c = Vector.cross(a, b);
            c.show();
            c.normalize();
            c.show();

            Console.WriteLine("Sphere : ");
            Sphere sphere = new Sphere(new Vector(0, 0, 0), 10);
            sphere.show();
            Vector start = new Vector(0, 0, -20);
            Vector dirVector = Vector.sub(sphere.centre, start);
            dirVector.normalize();
            Ray ray1 = new Ray(start, dirVector);

            Ray ray2 = new Ray(start, new Vector(0, 1, 0));

            Vector v1;
            Vector v2;
            //10. przecięcie sfery z promieniami
            Console.WriteLine($"Sphere and ray1: {ray1.intersect(sphere, out v1, out v2)}");
            //11 wyswietlenie koordynatow
            Console.WriteLine($"Intersection Sphere and ray1:");
            v1.show();
            v2.show();
            Console.WriteLine($"Sphere and ray2: {ray2.intersect(sphere, out v1, out v2)}");
            if (v1 != null && v2 != null)
            {
                v1.show();
                v2.show();
            }

            //12
            Ray ray3 = new Ray(new Vector(10, -10, 0), new Vector(-1, 0, 0));
            ray3.intersect(sphere, out v1, out v2);
            Console.WriteLine("Intersection Coordinates: ");
            v1.show();
            //13
            Plane plane = new Plane(new Vector(0, 0, 0), new Vector(0, 1, 1));
            //14
            Console.WriteLine("R2 and plane 1 Intersection Coordinates: {}");
            ray2.intersect(plane, out v1);
            v1.show();
            //15
            Triangle t1 = new Triangle(new Vector(0, 0, 0), new Vector(1, 0, 0), new Vector(0, 1, 0));

            Ray triangleRay = new Ray(new Vector(-1, 0.5, 0), new Vector(1, 0.5, 0));
            Console.WriteLine($"Triangle and ray1: {triangleRay.intersect(t1, out v1)}");
            Ray triangleRay1 = new Ray(new Vector(2, -1, 0), new Vector(2, 2, 0));
            Console.WriteLine($"Triangle and ray2: {triangleRay1.intersect(t1, out v1)}");
            Ray triangleRay2 = new Ray(new Vector(0, 0, -1), new Vector(0, 0, 1));
            Console.WriteLine($"Triangle and ray3: {triangleRay2.intersect(t1, out v1)}");

        }
    }

}
