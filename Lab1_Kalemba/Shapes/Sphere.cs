namespace RayTracing;
public class Sphere : Shape
{
    public Vector centre;
    public double radious;
    public Sphere(Vector p, double r)
    {
        centre = p;
        radious = r;
    }

    public override Vector getNormal(Vector point)
    {
        Vector normal = Vector.Sub(point, centre);
        normal.normalize();
        return normal;
    }

    public override IntersectionInfo Intersect(Ray ray)
    {
        IntersectionInfo intersectionInfo = new IntersectionInfo();
        Vector oc = Vector.Sub(ray.start, centre);
        double a = Vector.Dot(ray.dir, ray.dir);
        double b = 2 * Vector.Dot(oc, ray.dir);
        double c = Vector.Dot(oc, oc) - radious * radious;

        double discriminant = b * b - 4 * a * c;

        if (discriminant < 0) return intersectionInfo;
        if (discriminant == 0)
        {
            double t = -b / 2 * a;
            Vector distance = new Vector(ray.dir);
            distance.mag(t);
            Vector crossPoint0 = Vector.Add(ray.start, distance);

            intersectionInfo.SetIntersect(true);
            intersectionInfo.addIntersect(crossPoint0);
            return intersectionInfo;
        }

        double t1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
        double t2 = (-b + Math.Sqrt(discriminant)) / (2 * a);

        Vector distance1 = new Vector(ray.dir);
        distance1.mag(t1);
        Vector distance2 = new Vector(ray.dir);
        distance2.mag(t2);

        Vector crossPoint1 = Vector.Add(ray.start, distance1);
        Vector crossPoint2 = Vector.Add(ray.start, distance2);
        intersectionInfo.SetIntersect(true);
        intersectionInfo.addIntersect(crossPoint1);
        intersectionInfo.addIntersect(crossPoint2);
        return intersectionInfo;
    }


    public override void show()
    {
        centre.show();
        Console.WriteLine($"Radious : {radious}");
    }

}

