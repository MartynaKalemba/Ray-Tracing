namespace RayTracing;


public class Material
{

    public float specular_amount { get; } //[0,infinity]. 0 = matt
    public float specular_coeff { get; } //(0, infinity). for phong lighting a.k.a shineness.

    public float reflect_fraction { get; } //[0,1]. 1 == total reflection, i.e.

    public LightIntensity diffuse_colour { get; }
    public Material(LightIntensity diffuse_colour, float specular_amount = 0, float specular_coeff = 0, float reflect_fraction = 0)
    {

        this.diffuse_colour = diffuse_colour;
        this.specular_amount = specular_amount;
        this.specular_coeff = specular_coeff;
        this.reflect_fraction = reflect_fraction;
        reflect_fraction = Math.Clamp(reflect_fraction, 0, 1);

    }
    ~Material() { }



}

