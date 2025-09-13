namespace GeometryLib.Tests;

public class Matrix3x3Tests
{
    [Fact]
    public void Can_create_matrix_with_valid_values()
    {
        Matrix3x3 matrix = new Matrix3x3(
            1, 2, 3,
            4, 5, 6,
            7, 8, 9
        );

        Assert.Equal(1, matrix[0, 0]);
        Assert.Equal(2, matrix[0, 1]);
        Assert.Equal(3, matrix[0, 2]);
        Assert.Equal(4, matrix[1, 0]);
        Assert.Equal(5, matrix[1, 1]);
        Assert.Equal(6, matrix[1, 2]);
        Assert.Equal(7, matrix[2, 0]);
        Assert.Equal(8, matrix[2, 1]);
        Assert.Equal(9, matrix[2, 2]);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(3, 0)]
    [InlineData(0, -1)]
    [InlineData(0, 3)]
    public void Cannot_access_invalid_indices(int row, int col)
    {
        Matrix3x3 matrix = new Matrix3x3(
            1, 2, 3,
            4, 5, 6,
            7, 8, 9
        );

        Assert.Throws<ArgumentOutOfRangeException>(() => matrix[row, col]);
        Assert.Throws<ArgumentOutOfRangeException>(() => matrix[row, col] = 10);
    }

    [Fact]
    public void Identity_matrix_has_correct_values()
    {
        Matrix3x3 identity = Matrix3x3.Identity();

        Assert.Equal(1, identity[0, 0]);
        Assert.Equal(0, identity[0, 1]);
        Assert.Equal(0, identity[0, 2]);
        Assert.Equal(0, identity[1, 0]);
        Assert.Equal(1, identity[1, 1]);
        Assert.Equal(0, identity[1, 2]);
        Assert.Equal(0, identity[2, 0]);
        Assert.Equal(0, identity[2, 1]);
        Assert.Equal(1, identity[2, 2]);
    }

    [Theory]
    [MemberData(nameof(DeterminantTestData))]
    public void Can_calculate_determinant(Matrix3x3 matrix, double expected)
    {
        Assert.Equal(expected, matrix.Determinant(), precision: Matrix3x3.Precision);
    }

    public static TheoryData<Matrix3x3, double> DeterminantTestData()
    {
        return new TheoryData<Matrix3x3, double>
        {
            { Matrix3x3.Identity(), 1 },
            { new Matrix3x3(2, 0, 0, 0, 3, 0, 0, 0, 4), 24 },
            { new Matrix3x3(1, 2, 3, 4, 5, 6, 7, 8, 9), 0 },
            { new Matrix3x3(1, 0, 5, 2, 1, 6, 3, 4, 0), 1 },
        };
    }

    [Theory]
    [MemberData(nameof(TransformTestData))]
    public void Can_transform_vector(Matrix3x3 matrix, Vector3 vector, Vector3 expected)
    {
        Vector3 result = matrix.Transform(vector);
        Assert.Equal(expected, result);
    }

    public static TheoryData<Matrix3x3, Vector3, Vector3> TransformTestData()
    {
        return new TheoryData<Matrix3x3, Vector3, Vector3>
        {
            { Matrix3x3.Identity(), new Vector3(1, 2, 3), new Vector3(1, 2, 3) },
            
            { new Matrix3x3(2, 0, 0, 0, 3, 0, 0, 0, 4), new Vector3(1, 1, 1), new Vector3(2, 3, 4) },
            
            { new Matrix3x3(0, -1, 0, 1, 0, 0, 0, 0, 1), new Vector3(1, 0, 0), new Vector3(0, 1, 0) },
        };
    }

    [Theory]
    [MemberData(nameof(AddTestData))]
    public void Can_add_matrices(Matrix3x3 a, Matrix3x3 b, Matrix3x3 expected)
    {
        Matrix3x3 result = a.Add(b);
        Assert.Equal(expected, result);
    }

    public static TheoryData<Matrix3x3, Matrix3x3, Matrix3x3> AddTestData()
    {
        return new TheoryData<Matrix3x3, Matrix3x3, Matrix3x3>
        {
            {
                new Matrix3x3(1, 2, 3, 4, 5, 6, 7, 8, 9),
                new Matrix3x3(9, 8, 7, 6, 5, 4, 3, 2, 1),
                new Matrix3x3(10, 10, 10, 10, 10, 10, 10, 10, 10)
            },
            {
                Matrix3x3.Identity(),
                Matrix3x3.Identity(),
                new Matrix3x3(2, 0, 0, 0, 2, 0, 0, 0, 2)
            },
        };
    }

    [Theory]
    [MemberData(nameof(MultiplyScalarTestData))]
    public void Can_multiply_by_scalar(Matrix3x3 matrix, double scalar, Matrix3x3 expected)
    {
        Matrix3x3 result = matrix.Multiply(scalar);
        Assert.Equal(expected, result);
    }

    public static TheoryData<Matrix3x3, double, Matrix3x3> MultiplyScalarTestData()
    {
        return new TheoryData<Matrix3x3, double, Matrix3x3>
        {
            {
                new Matrix3x3(1, 2, 3, 4, 5, 6, 7, 8, 9),
                2,
                new Matrix3x3(2, 4, 6, 8, 10, 12, 14, 16, 18)
            },
            {
                Matrix3x3.Identity(),
                5,
                new Matrix3x3(5, 0, 0, 0, 5, 0, 0, 0, 5)
            },
            {
                new Matrix3x3(1, 2, 3, 4, 5, 6, 7, 8, 9),
                0,
                new Matrix3x3(0, 0, 0, 0, 0, 0, 0, 0, 0)
            },
        };
    }

    [Fact]
    public void Matrices_with_same_values_are_equal()
    {
        Matrix3x3 a = new Matrix3x3(1, 2, 3, 4, 5, 6, 7, 8, 9);
        Matrix3x3 b = new Matrix3x3(1, 2, 3, 4, 5, 6, 7, 8, 9);
        Matrix3x3 c = new Matrix3x3(1, 2, 3, 4, 5, 6, 7, 8, 10);

        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a.Equals(c));
        Assert.True(a != c);
    }

    [Fact]
    public void Null_matrix_is_not_equal()
    {
        Matrix3x3 matrix = new Matrix3x3(1, 2, 3, 4, 5, 6, 7, 8, 9);

        Assert.False(matrix.Equals(null));
        Assert.False(matrix == null);
        Assert.False(null == matrix);
        Assert.True(matrix != null);
        Assert.True(null != matrix);
    }

    [Fact]
    public void Can_get_hash_code()
    {
        Matrix3x3 a = new Matrix3x3(1, 2, 3, 4, 5, 6, 7, 8, 9);
        Matrix3x3 b = new Matrix3x3(1, 2, 3, 4, 5, 6, 7, 8, 9);
        Matrix3x3 c = new Matrix3x3(1, 2, 3, 4, 5, 6, 7, 8, 10);

        Assert.Equal(a.GetHashCode(), b.GetHashCode());
        Assert.NotEqual(a.GetHashCode(), c.GetHashCode());
    }

    [Fact]
    public void Can_to_string()
    {
        Matrix3x3 matrix = new Matrix3x3(1.123456, 2, 3, 4, 5, 6, 7, 8, 9);
        string result = matrix.ToString();

        Assert.Contains("1,123", result);
        Assert.Contains("2,000", result);
    }
}