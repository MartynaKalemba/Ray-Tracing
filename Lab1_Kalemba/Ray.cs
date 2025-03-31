namespace RayTracing;
public class Ray
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
    public bool intersect(Sphere s, out Vector v1, out Vector v2)
    {
        v1 = null;
        v2 = null;
        Vector oc = Vector.sub(start, s.centre);
        double a = Vector.dot(dir, dir);
        double b = 2 * Vector.dot(oc, dir);
        double c = Vector.dot(oc, oc) - s.radious * s.radious;

        double discriminant = b * b - 4 * a * c;

        if (discriminant < 0) return false;
        if (discriminant == 0)
        {
            double t = -b / 2 * a;
            Vector distance = new Vector(dir);
            distance.mag(t);
            Vector crossPoint0 = Vector.add(start, distance);
            v1 = crossPoint0;
            return true;
        }

        double t1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
        double t2 = (-b + Math.Sqrt(discriminant)) / (2 * a);

        Vector distance1 = new Vector(dir);
        distance1.mag(t1);
        Vector distance2 = new Vector(dir);
        distance2.mag(t2);

        Vector crossPoint1 = Vector.add(start, distance1);
        Vector crossPoint2 = Vector.add(start, distance2);
        v1 = crossPoint1;
        v2 = crossPoint2;
        return true;
    }
    public bool intersect(Triangle tri, out Vector barycentricCoords)
    {

        double t = 0;
        barycentricCoords = null;

        Vector edge1 = Vector.sub(tri.b, tri.a);
        Vector edge2 = Vector.sub(tri.c, tri.a);
        Vector h = Vector.cross(dir, edge2);
        double a = Vector.dot(edge1, h);

        if (Math.Abs(a) < 1e-6)
            return false;

        double f = 1.0f / a;
        Vector s = Vector.sub(start, tri.a);
        double u = f * Vector.dot(s, h);

        if (u < 0.0 || u > 1.0)
            return false;

        Vector q = Vector.cross(s, edge1);
        double v = f * Vector.dot(dir, q);

        if (v < 0.0 || u + v > 1.0)
            return false;

        t = f * Vector.dot(edge2, q);

        if (t > 1e-6)
        {
            barycentricCoords = new Vector(u, v, 1 - u - v);
            return true;
        }

        return false;
    }
    public bool intersect(Plane p, out Vector intersectionPoint)
    {
        double t = 0;
        intersectionPoint = null;


        double denominator = Vector.dot(p.normal, dir);
        if (Math.Abs(denominator) < 1e-6) // Avoid floating-point precision issues
            return false;

        Vector p0MinusStart = Vector.sub(p.point, start);
        t = Vector.dot(p.normal, p0MinusStart) / denominator;
        if (t < 0)
            return false;


        Vector distance = new Vector(dir);
        distance.mag(t);
        intersectionPoint = Vector.add(start, distance);
        return true;
    }

}
