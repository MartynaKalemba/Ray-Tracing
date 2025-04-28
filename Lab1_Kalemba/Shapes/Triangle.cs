namespace RayTracing;

public class Triangle : Shape
{
    public Vector a;
    public Vector b;
    public Vector c;


    public Triangle(Vector point1, Vector point2, Vector point3)
    {
        a = point1;
        b = point2;
        c = point3;

    }

    public override Vector getNormal(Vector point)
    {
        throw new NotImplementedException();
    }

    public override IntersectionInfo Intersect(Ray ray)
    {
        throw new NotImplementedException();
    }

    public override void show()
    {
        a.show();
        b.show();
        c.show();
    }
}