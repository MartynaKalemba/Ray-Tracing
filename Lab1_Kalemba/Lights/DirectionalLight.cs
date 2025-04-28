namespace RayTracing;

public class DirectionalLight : Light
{

    public Plane plane { get; }

    public DirectionalLight(Plane p, LightIntensity lightIntensity) : base(lightIntensity)
    {
        plane = p;
    }

    public override LightIntensity GetLightIntensity(Vector point)
    {
        return this.I;
    }
}
