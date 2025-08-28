namespace GeometryLib.Tests;

public class Triangle2DTests
{
    [Theory]
    [MemberData(nameof(ValidTriangleData))]
    public void CreateTriangle_WithValidPoints_ShouldSucceed(Point2D p1, Point2D p2, Point2D p3)
    {
        Triangle2D triangle = new Triangle2D(p1, p2, p3);
        Assert.NotNull(triangle);
    }

    [Theory]
    [MemberData(nameof(InvalidTriangleData))]
    public void CreateTriangle_WithInvalidPoints_ShouldThrowException(Point2D p1, Point2D p2, Point2D p3)
    {
        Assert.Throws<ArgumentException>(() => new Triangle2D(p1, p2, p3));
    }

    [Theory]
    [MemberData(nameof(TriangleSideData))]
    public void CalculateSideLengths_ShouldReturnCorrectValues(Point2D p1, Point2D p2, Point2D p3, 
        double expectedSide1, double expectedSide2, double expectedSide3)
    {
        Triangle2D triangle = new Triangle2D(p1, p2, p3);

        Assert.Equal(expectedSide1, triangle.Side1, 10);
        Assert.Equal(expectedSide2, triangle.Side2, 10);
        Assert.Equal(expectedSide3, triangle.Side3, 10);
    }

    [Theory]
    [MemberData(nameof(TrianglePerimeterData))]
    public void CalculatePerimeter_ShouldReturnCorrectSum(Point2D p1, Point2D p2, Point2D p3, double expectedPerimeter)
    {
        Triangle2D triangle = new Triangle2D(p1, p2, p3);
        Assert.Equal(expectedPerimeter, triangle.Perimeter, 10);
    }

    [Theory]
    [MemberData(nameof(TriangleAreaData))]
    public void CalculateArea_ShouldReturnCorrectValue(Point2D p1, Point2D p2, Point2D p3, double expectedArea)
    {
        Triangle2D triangle = new Triangle2D(p1, p2, p3);
        Assert.Equal(expectedArea, triangle.Area, 10);
    }

    [Theory]
    [MemberData(nameof(TriangleCenterData))]
    public void FindCentroid_ShouldReturnCorrectCenterPoint(Point2D p1, Point2D p2, Point2D p3, Point2D expectedCenter)
    {
        Triangle2D triangle = new Triangle2D(p1, p2, p3);
        Point2D centroid = triangle.Centroid;

        Assert.Equal(expectedCenter.X, centroid.X, 10);
        Assert.Equal(expectedCenter.Y, centroid.Y, 10);
    }

    [Theory]
    [MemberData(nameof(RightAngleTriangleData))]
    public void CheckRightAngle_ShouldDetectCorrectly(Point2D p1, Point2D p2, Point2D p3, bool hasRightAngle)
    {
        Triangle2D triangle = new Triangle2D(p1, p2, p3);
        Assert.Equal(hasRightAngle, triangle.IsRightAngled());
    }

    [Theory]
    [MemberData(nameof(PointContainmentData))]
    public void CheckPointContainment_ShouldReturnCorrectResult(Point2D p1, Point2D p2, Point2D p3, 
        Point2D testPoint, bool shouldBeContained)
    {
        Triangle2D triangle = new Triangle2D(p1, p2, p3);
        Assert.Equal(shouldBeContained, triangle.Contains(testPoint));
    }

    public static TheoryData<Point2D, Point2D, Point2D> ValidTriangleData()
    {
        return new TheoryData<Point2D, Point2D, Point2D>
        {
            { new(0, 0), new(3, 0), new(0, 4) },
            { new(1, 1), new(4, 1), new(2, 5) }
        };
    }

    public static TheoryData<Point2D, Point2D, Point2D> InvalidTriangleData()
    {
        return new TheoryData<Point2D, Point2D, Point2D>
        {
            { new(0, 0), new(1, 1), new(2, 2) },
            { new(0, 0), new(0, 0), new(1, 1) },
            { new(1, 1), new(1, 1), new(1, 1) },
            { new(2, 2), new(3, 3), new(4, 4) }
        };
    }

    public static TheoryData<Point2D, Point2D, Point2D, double, double, double> TriangleSideData() =>
        new TheoryData<Point2D, Point2D, Point2D, double, double, double>
        {
            { new(0, 0), new(3, 0), new(0, 4), 3, 5, 4 },
            { new(0, 0), new(2, 0), new(1, Math.Sqrt(3)), 2, 2, 2 },
            { new(1, 1), new(4, 1), new(1, 5), 3, 5, 4 }
        };

    public static TheoryData<Point2D, Point2D, Point2D, double> TrianglePerimeterData() =>
        new TheoryData<Point2D, Point2D, Point2D, double>
        {
            { new(0, 0), new(3, 0), new(0, 4), 12 },
            { new(0, 0), new(2, 0), new(1, Math.Sqrt(3)), 6 },
            { new(1, 1), new(4, 1), new(1, 5), 12 }
        };

    public static TheoryData<Point2D, Point2D, Point2D, double> TriangleAreaData() =>
        new TheoryData<Point2D, Point2D, Point2D, double>
        {
            { new(0, 0), new(3, 0), new(0, 4), 6 },
            { new(0, 0), new(2, 0), new(1, Math.Sqrt(3)), Math.Sqrt(3) },
            { new(1, 1), new(4, 1), new(1, 5), 6 }
        };

    public static TheoryData<Point2D, Point2D, Point2D, Point2D> TriangleCenterData() =>
        new TheoryData<Point2D, Point2D, Point2D, Point2D>
        {
            { new(0, 0), new(6, 0), new(0, 6), new(2, 2) },
            { new(1, 2), new(4, 6), new(7, 2), new(4, 10D / 3) },
            { new(0, 0), new(4, 0), new(0, 4), new(4D / 3, 4D / 3) }
        };

    public static TheoryData<Point2D, Point2D, Point2D, bool> RightAngleTriangleData() =>
        new TheoryData<Point2D, Point2D, Point2D, bool>
        {
            { new(0, 0), new(3, 0), new(0, 4), true },
            { new(1, 1), new(1, 4), new(5, 1), true },
            { new(1, 1), new(4, 5), new(0, 3), false },
            { new(2, 2), new(5, 2), new(2, 6), true }
        };

    public static TheoryData<Point2D, Point2D, Point2D, Point2D, bool> PointContainmentData() =>
        new TheoryData<Point2D, Point2D, Point2D, Point2D, bool>
        {
            { new(0, 0), new(3, 0), new(0, 3), new(1, 1), true },
            { new(0, 0), new(3, 0), new(0, 3), new(4, 4), false },
            { new(0, 0), new(3, 0), new(0, 3), new(0, 0), true },
            { new(0, 0), new(3, 0), new(0, 3), new(1.5, 0), true },
            { new(1, 1), new(4, 1), new(1, 4), new(2, 2), true }
        };
}