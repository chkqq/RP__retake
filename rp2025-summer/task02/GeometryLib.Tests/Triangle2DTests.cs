namespace GeometryLib.Tests;

public class Triangle2DTests
{
    [Theory]
    [MemberData(nameof(ConstructorSuccessTestData))]
    public void Constructor_Validates_Triangle_should_be_success(Point2D a, Point2D b, Point2D c)
    {
        Triangle2D triangle = new Triangle2D(a, b, c);
        Assert.NotNull(triangle);
    }

    [Theory]
    [MemberData(nameof(ConstructorNotSuccessTestData))]
    public void Constructor_Validates_Triangle_not_should_be_success(Point2D a, Point2D b, Point2D c)
    {
        Assert.Throws<ArgumentException>(() => new Triangle2D(a, b, c));
    }

    [Theory]
    [MemberData(nameof(SideLengthsTestData))]
    public void SideLengths_Are_Calculated_Correctly(Point2D a, Point2D b, Point2D c, double ab, double bc, double ca)
    {
        Triangle2D triangle = new Triangle2D(a, b, c);

        Assert.Equal(ab, triangle.SideAB, 10);
        Assert.Equal(bc, triangle.SideBC, 10);
        Assert.Equal(ca, triangle.SideCA, 10);
    }

    [Theory]
    [MemberData(nameof(PerimeterTestData))]
    public void Perimeter_Is_Calculated_Correctly(Point2D a, Point2D b, Point2D c, double expected)
    {
        Triangle2D triangle = new Triangle2D(a, b, c);
        Assert.Equal(expected, triangle.Perimeter, 10);
    }

    [Theory]
    [MemberData(nameof(AreaTestData))]
    public void Area_Is_Calculated_Correctly(Point2D a, Point2D b, Point2D c, double expected)
    {
        Triangle2D triangle = new Triangle2D(a, b, c);
        Assert.Equal(expected, triangle.Area, 10);
    }

    [Theory]
    [MemberData(nameof(CentroidTestData))]
    public void Centroid_Is_Calculated_Correctly(Point2D a, Point2D b, Point2D c, Point2D expected)
    {
        Triangle2D triangle = new Triangle2D(a, b, c);
        Point2D centroid = triangle.Centroid;

        Assert.Equal(expected.X, centroid.X, 10);
        Assert.Equal(expected.Y, centroid.Y, 10);
    }

    [Theory]
    [MemberData(nameof(RightAngledTestData))]
    public void IsRightAngled_Detects_Correctly(Point2D a, Point2D b, Point2D c, bool expected)
    {
        Triangle2D triangle = new Triangle2D(a, b, c);
        Assert.Equal(expected, triangle.IsRightAngled());
    }

    [Theory]
    [MemberData(nameof(ContainsTestData))]
    public void Contains_Detects_Point_Position(Point2D a, Point2D b, Point2D c, Point2D point, bool expected)
    {
        Triangle2D triangle = new Triangle2D(a, b, c);
        Assert.Equal(expected, triangle.Contains(point));
    }

    public static TheoryData<Point2D, Point2D, Point2D> ConstructorSuccessTestData()
    {
        return new TheoryData<Point2D, Point2D, Point2D>
        {
            { new(0, 0), new(3, 0), new(0, 4) },
        };
    }

    public static TheoryData<Point2D, Point2D, Point2D> ConstructorNotSuccessTestData()
    {
        return new TheoryData<Point2D, Point2D, Point2D>
        {
            { new(0, 0), new(1, 1), new(2, 2) },
            { new(0, 0), new(0, 0), new(1, 1) },
            { new(1, 1), new(1, 1), new(1, 1) }, 
        };
    }

    public static TheoryData<Point2D, Point2D, Point2D, double, double, double> SideLengthsTestData() =>
        new TheoryData<Point2D, Point2D, Point2D, double, double, double>
        {
            { new(0, 0), new(3, 0), new(0, 4), 3, 5, 4 },
            { new(0, 0), new(2, 0), new(1, Math.Sqrt(3)), 2, 2, 2 },
        };

    public static TheoryData<Point2D, Point2D, Point2D, double> PerimeterTestData() =>
        new TheoryData<Point2D, Point2D, Point2D, double>
        {
            { new(0, 0), new(3, 0), new(0, 4), 12 },
            { new(0, 0), new(2, 0), new(1, Math.Sqrt(3)), 6 },
        };

    public static TheoryData<Point2D, Point2D, Point2D, double> AreaTestData() =>
        new TheoryData<Point2D, Point2D, Point2D, double>
        {
            { new(0, 0), new(3, 0), new(0, 4), 6 },
            { new(0, 0), new(2, 0), new(1, Math.Sqrt(3)), Math.Sqrt(3) },
        };

    public static TheoryData<Point2D, Point2D, Point2D, Point2D> CentroidTestData() =>
        new TheoryData<Point2D, Point2D, Point2D, Point2D>
        {
            { new(0, 0), new(6, 0), new(0, 6), new(2, 2) },
            { new(1, 2), new(4, 6), new(7, 2), new(4, 10D / 3) },
        };

    public static TheoryData<Point2D, Point2D, Point2D, bool> RightAngledTestData() =>
        new TheoryData<Point2D, Point2D, Point2D, bool>
        {
            { new(0, 0), new(3, 0), new(0, 4), true },
            { new(1, 1), new(1, 4), new(5, 1), true },
            { new(1, 1), new(4, 5), new(0, 3), false },
        };

    public static TheoryData<Point2D, Point2D, Point2D, Point2D, bool> ContainsTestData() =>
        new TheoryData<Point2D, Point2D, Point2D, Point2D, bool>
        {
            { new(0, 0), new(3, 0), new(0, 3), new(1, 1), true },
            { new(0, 0), new(3, 0), new(0, 3), new(4, 4), false },
            { new(0, 0), new(3, 0), new(0, 3), new(0, 0), true },
            { new(0, 0), new(3, 0), new(0, 3), new(1.5, 0), true },
        };
}