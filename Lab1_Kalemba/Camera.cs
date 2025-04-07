using System.Drawing;

namespace RayTracing;

public abstract class Camera
{
    private Vector position;
    private Vector target;
    private Vector up;
    private float nearPlane;
    private float farPlane;
    private float fov; //what is this?

    public Vector Position
    {
        get { return position; }
        set { position = value; }
    }

    public Vector Target
    {
        get { return target; }
        set { target = value; }
    }

    public Vector Up
    {
        get { return up; }
        set { up = value; }
    }

    public float NearPlane
    {
        get { return nearPlane; }
        set { nearPlane = value; }
    }

    public float FarPlane
    {
        get { return farPlane; }
        set { farPlane = value; }
    }

    public float Fov
    {
        get { return fov; }
        set { fov = value; }
    }


    public Camera()
    {
        this.position = new Vector(0, 0, 0);
        this.target = new Vector(0, 0, 1);
        this.nearPlane = 1;
        this.farPlane = 1000;
        this.up = new Vector(0, 1, 0);
    }
    public Camera(Vector position, Vector target)
    {
        this.position = position;
        this.target = target;
        this.nearPlane = 1;
        this.farPlane = 1000;
        this.up = new Vector(0, 1, 0);
    }

    public abstract void See();
}
