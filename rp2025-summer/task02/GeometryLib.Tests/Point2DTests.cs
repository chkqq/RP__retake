namespace GeometryLib.Tests;

public class Point2DTests
{
    [Theory]
    [MemberData(nameof(DistanceToTestData))]
    public void Can_calculate_distance_to(Point2D point1, Point2D point2, double expected)
    {
        Assert.Equal(expected, point1.DistanceTo(point2));
    }

    public static TheoryData<Point2D, Point2D, double> DistanceToTestData()
    {
        return new TheoryData<Point2D, Point2D, double>
        {
            { new Point2D(0, 0), new Point2D(3, 4), 5 },
            { new Point2D(-1, -1), new Point2D(2, 3), 5 },
            { new Point2D(5, 5), new Point2D(5, 5), 0 },
        };
    }
}