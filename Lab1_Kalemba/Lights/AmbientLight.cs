namespace RayTracing;

public class AmbientLight : Light
{
    public AmbientLight(LightIntensity li) : base(li)
    {

    }

    public override LightIntensity GetLightIntensity(Vector point)
    {
        return this.I;
    }
}
