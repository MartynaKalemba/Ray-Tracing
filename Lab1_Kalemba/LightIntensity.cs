namespace RayTracing;

public class LightIntensity
{
    protected
    double r, g, b;

    public LightIntensity(double R = 0, double G = 0, double B = 0)
    {
        if (R > 1) _ = 1;
        if (G > 1) _ = 1;
        if (B > 1) _ = 1;
        if (R < 0) _ = 0;
        if (G < 0) _ = 0;
        if (B < 0) _ = 0;
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
        double red = a.r + b.r;
        double green = a.g + b.g;
        double blue = a.b + b.b;
        // if (red > 1) _ = 1;
        // if (green > 1) _ = 1;
        // if (blue > 1) _ = 1;
        // if (red < 0) _ = 0;
        // if (green < 0) _ = 0;
        // if (blue < 0) _ = 0;
        return new LightIntensity(red, green, blue);
    }

    public static LightIntensity operator -(LightIntensity a, LightIntensity b)
    {
        double red = a.r - b.r;
        double green = a.g - b.g;
        double blue = a.b - b.b;
        // if (red > 1) _ = 1;
        // if (green > 1) _ = 1;
        // if (blue > 1) _ = 1;
        // if (red < 0) _ = 0;
        // if (green < 0) _ = 0;
        // if (blue < 0) _ = 0;
        return new LightIntensity(red, green, blue);
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
        double red = a.r * scalar;
        double green = a.g * scalar;
        double blue = a.b * scalar;
        // if (red > 1) _ = 1;
        // if (green > 1) _ = 1;
        // if (blue > 1) _ = 1;
        // if (red < 0) _ = 0;
        // if (green < 0) _ = 0;
        // if (blue < 0) _ = 0;

        return new LightIntensity(red, green, blue);
    }

    public static LightIntensity operator /(LightIntensity a, double scalar)
    {
        if (scalar != 0)
        {
            double red = a.r / scalar;
            double green = a.g / scalar;
            double blue = a.b / scalar;
            if (red > 1) _ = 1;
            if (green > 1) _ = 1;
            if (blue > 1) _ = 1;
            if (red < 0) _ = 0;
            if (green < 0) _ = 0;
            if (blue < 0) _ = 0;

            return new LightIntensity(red, green, blue);
        }
        return new LightIntensity(0, 0, 0);

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
