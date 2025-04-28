namespace RayTracing;
public class Ray
{
    public Vector start;
    public Vector dir;

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

    public bool intersect(Triangle tri, out Vector barycentricCoords, out Vector v2)
    {

        double t = 0;
        barycentricCoords = null;
        v2 = null;

        Vector edge1 = Vector.Sub(tri.b, tri.a);
        Vector edge2 = Vector.Sub(tri.c, tri.a);
        Vector h = Vector.cross(dir, edge2);
        double a = Vector.Dot(edge1, h);

        if (Math.Abs(a) < 1e-6)
            return false;

        double f = 1.0f / a;
        Vector s = Vector.Sub(start, tri.a);
        double u = f * Vector.Dot(s, h);

        if (u < 0.0 || u > 1.0)
            return false;

        Vector q = Vector.cross(s, edge1);
        double v = f * Vector.Dot(dir, q);

        if (v < 0.0 || u + v > 1.0)
            return false;

        t = f * Vector.Dot(edge2, q);

        if (t > 1e-6)
        {
            barycentricCoords = new Vector(u, v, 1 - u - v);
            return true;
        }

        return false;
    }
    public bool intersect(Plane p, out Vector intersectionPoint, out Vector v2)
    {
        double t = 0;
        intersectionPoint = null;
        v2 = null;


        double denominator = Vector.Dot(p.normal, dir);
        if (Math.Abs(denominator) < 1e-6) // Avoid floating-point precision issues
            return false;

        Vector p0MinusStart = Vector.Sub(p.point, start);
        t = Vector.Dot(p.normal, p0MinusStart) / denominator;
        if (t < 0)
            return false;


        Vector distance = new Vector(dir);
        distance.mag(t);
        intersectionPoint = Vector.Add(start, distance);
        return true;
    }

}
