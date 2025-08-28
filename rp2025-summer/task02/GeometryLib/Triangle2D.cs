using System.Drawing;
using System.Runtime.InteropServices;

namespace GeometryLib;

public class Triangle2D
{
    private readonly Point2D a;
    private readonly Point2D b;
    private readonly Point2D c;

    public Triangle2D(Point2D a, Point2D b, Point2D c)
    {
        double area = (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
        if (Math.Abs(area) < 1e-10)
        {
            throw new ArgumentException("Все три точки лежат на одной прямой");
        }

        this.a = a;
        this.b = b;
        this.c = c;
    }

    public double Side1
    {
        get
        {
            return a.DistanceTo(b);
        }
    }

    public double Side2
    {
        get
        {
            return b.DistanceTo(c);
        }
    }

    public double Side3
    {
        get
        {
            return c.DistanceTo(a);
        }
    }

    public double Perimeter
    {
        get
        {
            return this.Side1 + this.Side2 + this.Side3;
        }
    }

    public double Area
    {
        get
        {
            double p = this.Perimeter / 2;
            return Math.Sqrt(p * (p - this.Side1) * (p - this.Side2) * (p - this.Side3));
        }
    }

    public Point2D Centroid
    {
        get
        {
            double gx = (a.X + b.X + c.X) / 3;
            double gy = (a.Y + b.Y + c.Y) / 3;
            return new Point2D(x: gx, y: gy);
        }
    }

    public bool IsRightAngled()
    {
        bool firstOption = Math.Pow(this.Side1, 2) == Math.Pow(this.Side2, 2) + Math.Pow(this.Side3, 2);
        bool secondOption = Math.Pow(this.Side2, 2) == Math.Pow(this.Side1, 2) + Math.Pow(this.Side3, 2);
        bool thirdOption = Math.Pow(this.Side3, 2) == Math.Pow(this.Side1, 2) + Math.Pow(this.Side2, 2);
        return firstOption || secondOption || thirdOption;
    }

    public bool Contains(Point2D point)
    {
        double areaABC = TriangleArea(a, b, c);
        double areaPAB = TriangleArea(point, a, b);
        double areaPBC = TriangleArea(point, b, c);
        double areaPCA = TriangleArea(point, c, a);

        return Math.Abs(areaABC - (areaPAB + areaPBC + areaPCA)) < 1e-10;
    }

    private static double TriangleArea(Point2D a, Point2D b, Point2D c)
    {
        return Math.Abs((b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X)) / 2.0;
    }
}