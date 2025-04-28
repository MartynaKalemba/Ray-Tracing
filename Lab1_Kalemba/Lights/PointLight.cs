namespace RayTracing;

public class PointLight : Light
{
    Vector point;
    double radious;

    double fade1;
    double fade2;


    PointLight(Vector p, double r, LightIntensity lightIntensity, double f1 = 0, double f2 = 1) : base(lightIntensity)
    {
        point = p;
        radious = r;
        fade1 = f1;
        fade2 = f2;
    }


    public override LightIntensity GetLightIntensity(Vector point)
    {
        Vector distanceV = Vector.Sub(this.point, point);
        double distance = distanceV.length();

        return this.I / (fade1 + fade2 * distance);
    }
}
