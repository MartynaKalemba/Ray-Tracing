namespace RayTracing;

public abstract class Light
{
    protected LightIntensity light;

    public LightIntensity I => light;

    public Light()
    {
        light = new LightIntensity();
    }
    public Light(LightIntensity i)
    {
        light = i;
    }

    public abstract LightIntensity GetLightIntensity(Vector point);

}
