namespace RayTracing;
 public class Sphere
        {
            public Vector centre;
            public double radious;
            public Sphere(Vector p, double r)
            {
                centre = p;
                radious = r;
            }
            public void show(){
                centre.show();
                Console.WriteLine($"Radious : {radious}");
            }
        }

