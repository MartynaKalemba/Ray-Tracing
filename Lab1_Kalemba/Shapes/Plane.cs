namespace RayTracing;


public class Plane : Shape
{

    public Vector point;
    public Vector normal;
    public Plane(Vector p1, Vector n)
    {
        point = p1;
        normal = n;
    }

    public override Vector getNormal(Vector point)
    {
        //We should check if the point is on the normal to generalize the class
        return normal;
    }

    public override IntersectionInfo Intersect(Ray ray)
    {
        double t = 0;
        IntersectionInfo intersectionInfo = new IntersectionInfo();


        double denominator = Vector.Dot(normal, ray.dir);
        if (Math.Abs(denominator) < 1e-6)
            return intersectionInfo;

        Vector p0MinusStart = Vector.Sub(point, ray.start);
        t = Vector.Dot(normal, p0MinusStart) / denominator;
        if (t < 0)
            return intersectionInfo;


        Vector distance = new Vector(ray.dir);
        distance.mag(t);
        intersectionInfo.SetIntersect(true);
        intersectionInfo.addIntersect(Vector.Add(ray.start, distance));
        return intersectionInfo;
    }

    public override void show()
    {
        Console.WriteLine("Plane --\nPoint: ");
        point.show();
        Console.WriteLine("Normal: ");
        normal.show();
    }
}

