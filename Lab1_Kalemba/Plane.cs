namespace RayTracing;


public class Plane
{

    public Vector point;
    public Vector normal;
    public Plane(Vector p1, Vector n)
    {
        point = p1;
        normal = n;
    }
}

