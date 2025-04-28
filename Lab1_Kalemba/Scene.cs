namespace RayTracing;


public class Scene
{
    public AmbientLight AmbientLight { get; }
    public List<Primitive> Primitives { get; }
    public List<Light> Lights { get; }
    public LightIntensity? BgColor { get; }

    public Scene()
    {
        AmbientLight = null;
        Primitives = new List<Primitive>();
        Lights = new List<Light>();


    }
    public Scene(List<Primitive> p, List<Light> ll, AmbientLight al, LightIntensity? bg)
    {
        Primitives = p;
        Lights = ll;
        AmbientLight = al;
        BgColor = bg;
    }

    public void add(Primitive p)
    {
        Primitives.Add(p);
    }
    public void add(Light l)
    {
        Lights.Add(l);
    }

}

