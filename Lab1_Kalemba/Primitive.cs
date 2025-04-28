namespace RayTracing;


public class Primitive
{
    Shape shape;
    Material material;

    public Shape GetShape() => shape;
    public Material GetMaterial() => material;

    public Primitive(Shape s, Material m)
    {
        shape = s;
        material = m;
    }

}

