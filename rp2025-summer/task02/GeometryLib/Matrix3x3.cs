using System.Diagnostics.CodeAnalysis;

namespace GeometryLib;

public class Matrix3x3 : IEquatable<Matrix3x3>
{
    public const double Tolerance = 1e-10;
    public const int Precision = 10;

    private readonly double[,] _matrix;

    public Matrix3x3(double m00, double m01, double m02,
                     double m10, double m11, double m12,
                     double m20, double m21, double m22)
    {
        _matrix = new double[3, 3]
        {
            { m00, m01, m02 },
            { m10, m11, m12 },
            { m20, m21, m22 }
        };
    }

    public double this[int row, int col]
    {
        get
        {
            ValidateIndices(row, col);
            return _matrix[row, col];
        }
        set
        {
            ValidateIndices(row, col);
            _matrix[row, col] = value;
        }
    }

    private static void ValidateIndices(int row, int col)
    {
        if (row < 0 || row > 2 || col < 0 || col > 2)
        {
            throw new ArgumentOutOfRangeException("Row and column indices must be between 0 and 2");
        }
    }

    public static Matrix3x3 Identity()
    {
        return new Matrix3x3(
            1, 0, 0,
            0, 1, 0,
            0, 0, 1
        );
    }

    public double Determinant()
    {
        double a = _matrix[0, 0], b = _matrix[0, 1], c = _matrix[0, 2];
        double d = _matrix[1, 0], e = _matrix[1, 1], f = _matrix[1, 2];
        double g = _matrix[2, 0], h = _matrix[2, 1], i = _matrix[2, 2];

        return a * (e * i - f * h) - b * (d * i - f * g) + c * (d * h - e * g);
    }

    public Vector3 Transform(Vector3 vector)
    {
        double x = _matrix[0, 0] * vector.X + _matrix[0, 1] * vector.Y + _matrix[0, 2] * vector.Z;
        double y = _matrix[1, 0] * vector.X + _matrix[1, 1] * vector.Y + _matrix[1, 2] * vector.Z;
        double z = _matrix[2, 0] * vector.X + _matrix[2, 1] * vector.Y + _matrix[2, 2] * vector.Z;

        return new Vector3(x, y, z);
    }

    public Matrix3x3 Add(Matrix3x3 other)
    {
        return new Matrix3x3(
            _matrix[0, 0] + other[0, 0], _matrix[0, 1] + other[0, 1], _matrix[0, 2] + other[0, 2],
            _matrix[1, 0] + other[1, 0], _matrix[1, 1] + other[1, 1], _matrix[1, 2] + other[1, 2],
            _matrix[2, 0] + other[2, 0], _matrix[2, 1] + other[2, 1], _matrix[2, 2] + other[2, 2]
        );
    }

    public Matrix3x3 Multiply(double scalar)
    {
        return new Matrix3x3(
            _matrix[0, 0] * scalar, _matrix[0, 1] * scalar, _matrix[0, 2] * scalar,
            _matrix[1, 0] * scalar, _matrix[1, 1] * scalar, _matrix[1, 2] * scalar,
            _matrix[2, 0] * scalar, _matrix[2, 1] * scalar, _matrix[2, 2] * scalar
        );
    }

    public bool Equals(Matrix3x3 other)
    {
        if (other is null)
            return false;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (Math.Abs(_matrix[i, j] - other[i, j]) > Tolerance)
                    return false;
            }
        }
        return true;
    }

    public override bool Equals([NotNullWhen(true)] object obj)
    {
        return obj is Matrix3x3 other && Equals(other);
    }

    public override int GetHashCode()
    {
        HashCode hash = new HashCode();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                hash.Add(_matrix[i, j]);
            }
        }
        return hash.ToHashCode();
    }

    public static bool operator ==(Matrix3x3 left, Matrix3x3 right)
    {
        if (left is null)
            return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(Matrix3x3 left, Matrix3x3 right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Возвращает строковое представление матрицы
    /// </summary>
    public override string ToString()
    {
        return $"[{_matrix[0, 0]:F3}, {_matrix[0, 1]:F3}, {_matrix[0, 2]:F3}]\n" +
               $"[{_matrix[1, 0]:F3}, {_matrix[1, 1]:F3}, {_matrix[1, 2]:F3}]\n" +
               $"[{_matrix[2, 0]:F3}, {_matrix[2, 1]:F3}, {_matrix[2, 2]:F3}]";
    }
}