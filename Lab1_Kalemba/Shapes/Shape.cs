namespace RayTracing;


public abstract class Shape
{
    public abstract IntersectionInfo Intersect(Ray ray);

    public abstract void show();

    public abstract Vector getNormal(Vector point);

}

