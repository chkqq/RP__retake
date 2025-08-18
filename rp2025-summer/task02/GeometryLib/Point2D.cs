namespace GeometryLib;

public struct Point2D(double x, double y)
{
    public double X { get; } = x;

    public double Y { get; } = y;

    public double DistanceTo(Point2D point)
    {
        return Math.Sqrt(Math.Pow(X - point.X, 2) + Math.Pow(Y - point.Y, 2));
    }
}