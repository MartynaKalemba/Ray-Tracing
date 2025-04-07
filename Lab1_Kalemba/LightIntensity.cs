namespace RayTracing;

public class LightIntensity
{
    protected
    double r, g, b;

    public LightIntensity(double R = 0, double G = 0, double B = 0)
    {
        r = R;
        g = G;
        b = B;
    }
    public LightIntensity(LightIntensity other)
    {
        r = other.R;
        g = other.G;
        b = other.B;
    }

    public double R => r;
    public double G => g;
    public double B => b;

    public static LightIntensity operator +(LightIntensity a, LightIntensity b)
    {
        return new LightIntensity(a.r + b.r, a.g + b.g, a.b + b.b);
    }

    public static LightIntensity operator -(LightIntensity a, LightIntensity b)
    {
        return new LightIntensity(a.r - b.r, a.g - b.g, a.b - b.b);
    }

    public static LightIntensity operator *(LightIntensity a, LightIntensity b)
    {
        return new LightIntensity(a.r * b.r, a.g * b.g, a.b * b.b);
    }

    public static LightIntensity operator /(LightIntensity a, LightIntensity b)
    {
        return new LightIntensity(a.r / b.r, a.g / b.g, a.b / b.b);
    }

    public static LightIntensity operator *(LightIntensity a, double scalar)
    {
        return new LightIntensity(a.r * scalar, a.g * scalar, a.b * scalar);
    }

    public static LightIntensity operator /(LightIntensity a, double scalar)
    {
        return new LightIntensity(a.r / scalar, a.g / scalar, a.b / scalar);
    }

    public void set(double R, double G, double B)
    {
        r = R;
        g = G;
        b = B;
    }

    public void Add(LightIntensity other)
    {
        r += other.R;
        g += other.G;
        b += other.B;
    }

    public void Divide(float factor)
    {
        r /= factor;
        g /= factor;
        b /= factor;
    }


}
