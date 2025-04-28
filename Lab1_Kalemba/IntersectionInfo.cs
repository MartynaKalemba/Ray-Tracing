namespace RayTracing;


public class IntersectionInfo
{
    bool intersect;

    List<Vector> IntersectionPoints;

    public bool doesIntersect() => intersect;
    public List<Vector> GetPoints() => IntersectionPoints;
    public void SetIntersect(bool value) => intersect = value;
    public void SetPoints(List<Vector> points) => IntersectionPoints = points;

    public IntersectionInfo()
    {
        intersect = false;
        IntersectionPoints = new List<Vector>();
    }
    public void addIntersect(Vector v)
    {
        IntersectionPoints.Add(v);
    }
    public void show()
    {
        Console.WriteLine($"Intersection: {intersect}");
        if (!intersect) return;
        for (int i = 0; i < IntersectionPoints.Count; i++)
        {
            Console.WriteLine($"Coordinates: {IntersectionPoints[i]}");
        }

    }
    public Vector getClosestIntersection(Vector point, out double minDistance)
    {
        Vector closestPoint = IntersectionPoints.First();
        minDistance = Vector.Sub(IntersectionPoints.First(), point).length();

        for (int i = 0; i < IntersectionPoints.Count; i++)
        {
            double d1 = Vector.Sub(IntersectionPoints[i], point).length();
            if (d1 < minDistance)
            {
                minDistance = d1;
                closestPoint = IntersectionPoints[i];
            }
        }


        return closestPoint;
    }

}

