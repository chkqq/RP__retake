using System.Drawing;
using System.Runtime.InteropServices;

namespace GeometryLib;

public class Triangle2D
{
    public Point2D A { get; }
    public Point2D B { get; }
    public Point2D C { get; }

    public Triangle2D(Point2D a, Point2D b, Point2D c)
    {
        double area = (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
        if (Math.Abs(area) < 1e-10)
        {
            throw new ArgumentException("Все три точки лежат на одной прямой");
        }

        A = a;
        B = b;
        C = c;
    }

    public double Side1
    {
        get
        {
            return A.DistanceTo(B);
        }
    }

    public double Side2
    {
        get
        {
            return B.DistanceTo(C);
        }
    }

    public double Side3
    {
        get
        {
            return C.DistanceTo(A);
        }
    }

    public double Perimeter
    {
        get
        {
            return Side1 + Side2 + Side3;
        }
    }

    public double Area
    {
        get
        {
            double p = Perimeter / 2;
            return Math.Sqrt(p * (p - Side1) * (p - Side2) * (p - Side3));
        }
    }

    public Point2D Centroid
    {
        get
        {
            double gx = (A.X + B.X + C.X) / 3;
            double gy = (A.Y + B.Y + C.Y) / 3;
            return new Point2D(x: gx, y: gy);
        }
    }

    public bool IsRightAngled()
    {
        bool firstOption = Math.Pow(Side1, 2) == Math.Pow(Side2, 2) + Math.Pow(Side3, 2);
        bool secondOption = Math.Pow(Side2, 2) == Math.Pow(Side1, 2) + Math.Pow(Side3, 2);
        bool thirdOption = Math.Pow(Side3, 2) == Math.Pow(Side1, 2) + Math.Pow(Side2, 2);
        return firstOption || secondOption || thirdOption;
    }

    public bool Contains(Point2D point)
    {
        double areaABC = TriangleArea(A, B, C);
        double areaPAB = TriangleArea(point, A, B);
        double areaPBC = TriangleArea(point, B, C);
        double areaPCA = TriangleArea(point, C, A);

        return Math.Abs(areaABC - (areaPAB + areaPBC + areaPCA)) < 1e-10;
    }

    private static double TriangleArea(Point2D a, Point2D b, Point2D c)
    {
        return Math.Abs((b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X)) / 2.0;
    }
}