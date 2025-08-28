namespace GeometryLib.Tests;

public class Point2DTests
{
    [Theory]
    [MemberData(nameof(DistanceCalculationTestData))]
    public void Calculate_distance_between_points(Point2D first, Point2D second, double expectedDistance)
    {
        double actualDistance = first.DistanceTo(second);

        Assert.Equal(expectedDistance, actualDistance, 5); // 5 decimal places precision
    }

    [Theory]
    [MemberData(nameof(EdgeCaseDistanceData))]
    public void Handle_edge_cases_in_distance_calculation(Point2D first, Point2D second, double expected)
    {
        double result = first.DistanceTo(second);
        Assert.Equal(expected, result, 5);
    }

    [Fact]
    public void Distance_to_self_should_be_zero()
    {
        Point2D point = new Point2D(7, 13);

        Assert.Equal(0, point.DistanceTo(point));
    }

    [Fact]
    public void Distance_should_be_symmetric()
    {
        Point2D pointA = new Point2D(2, 5);
        Point2D pointB = new Point2D(7, 9);

        Assert.Equal(pointA.DistanceTo(pointB), pointB.DistanceTo(pointA));
    }

    public static TheoryData<Point2D, Point2D, double> DistanceCalculationTestData()
    {
        return new TheoryData<Point2D, Point2D, double>
        {
            { new Point2D(1, 2), new Point2D(4, 6), 5 },          // 3-4-5 triangle
            { new Point2D(-3, 2), new Point2D(1, -1), 5 },         // Different quadrant
            { new Point2D(0, 0), new Point2D(0, 10), 10 },         // Vertical line
            { new Point2D(5, 0), new Point2D(15, 0), 10 }          // Horizontal line
        };
    }

    public static TheoryData<Point2D, Point2D, double> EdgeCaseDistanceData()
    {
        return new TheoryData<Point2D, Point2D, double>
        {
            { new Point2D(0, 0), new Point2D(0, 0), 0 },           // Same point
            { new Point2D(double.MaxValue, 0), new Point2D(double.MinValue, 0), double.PositiveInfinity },
            { new Point2D(1.5, 2.5), new Point2D(4.5, 6.5), 5 }    // Decimal coordinates
        };
    }
}