using System.Net.Http.Headers;

namespace RayTracing;

public class Vector
{
    public double x;
    public double y;
    public double z;

    //constructors
    public Vector(Vector v)
    {
        x = v.x;
        y = v.y;
        z = v.z;
    }
    public Vector(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public void add(Vector p2)
    {
        x += p2.x;
        y += p2.y;
        z += p2.z;
    }
    public static Vector add(Vector p1, Vector p2)
    {
        return new Vector(p1.x + p2.x, p1.y + p2.y, p1.z + p2.z);
    }
    public void sub(Vector p2)
    {
        x -= p2.x;
        y -= p2.y;
        z -= p2.z;
    }
    public static Vector sub(Vector p1, Vector p2)
    {
        Vector p3 = new Vector(
        p1.x - p2.x,
        p1.y - p2.y,
        p1.z - p2.z
        );
        return p3;
    }
    public void mag(double s)
    {
        x *= s;
        y *= s;
        z *= s;
    }
    public void div(double s)
    {
        if (s != 0)
        {
            x /= s;
            y /= s;
            z /= s;
        }
        else
        {
            Console.WriteLine("Cannot div by 0!");
        }

    }
    public double scalar(Vector a, Vector b)
    {
        double s = a.x * b.x + a.y * b.y + a.z * b.z;
        return s;
    }

    public double angle(Vector a, Vector b)
    {
        double cos = scalar(a, b) / (a.length() * b.length());

        return Math.Acos(cos) / Math.PI * 180;
    }
    public double length()
    {
        return Math.Sqrt(x * x + y * y + z * z);
    }
    public void normalize()
    {
        double length = this.length();
        if (length == 0)
        {
            Console.WriteLine("Cannot normalize vector 0");
        }

        div(length);

    }

    public static Vector cross(Vector a, Vector b)
    {
        Vector c = new Vector(1, 1, 1);
        c.x = a.y * b.z - a.z * b.y;
        c.y = a.z * b.z - a.z * b.z;
        c.z = a.x * b.y - a.y * b.x;
        return c;
    }
    public static double dot(Vector a, Vector b)
    {

        return a.x * b.x + a.y * b.y + a.z * b.z;
    }

    public void show()
    {

        Console.WriteLine($"x: {x}, y: {y}, z: {z}");
    }
}

