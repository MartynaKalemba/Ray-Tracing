namespace RayTracing;
using SkiaSharp;

public class Image
{
    SKBitmap bitmap;
    int sizex;
    int sizey;

    public int Width => sizex;
    public int Height => sizey;

    //Należy teraz obliczyć kolor na podstawie natężenia:
    public void setPixel(int x, int y, LightIntensity pixel)
    {
        if (x >= sizex || y >= sizey) return;
        int red, green, blue;
        //konwersja na 0-255
        red = (int)(pixel.R * 255);
        green = (int)(pixel.G * 255);
        blue = (int)(pixel.B * 255);
        SKColor color = new SKColor((byte)red, (byte)green, (byte)blue);
        bitmap.SetPixel(x, y, color);
    }
    public void drawSquare(LightIntensity pixel)
    {

        for (int i = 0; i < 40; i++)
        {
            setPixel(1 + i, 1, pixel);
            setPixel(1, 1 + i, pixel);
            setPixel(1 + i, 1 + i, pixel);
            setPixel(40, 1 + i, pixel);
            setPixel(1 + i, 40, pixel);
        }
        setPixel(1, 1, pixel);
        setPixel(1, 3, pixel);
    }
    public void saveImage(string name)
    {
        using var image = SKImage.FromBitmap(bitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), name);
        File.WriteAllBytes(filePath, data.ToArray());
        Console.WriteLine("Image saved at: " + filePath);

    }
    public Image(int x, int y)
    {
        sizex = x;
        sizey = y;
        bitmap = new SKBitmap(sizex, sizey, SKColorType.Rgba8888, SKAlphaType.Premul);
    }
}
