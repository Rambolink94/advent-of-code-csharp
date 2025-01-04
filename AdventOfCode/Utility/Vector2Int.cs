using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace AdventOfCode.Utility;

public readonly struct Vector2Int : IEquatable<Vector2Int>, IFormattable
{
    private readonly int _x;
    private readonly int _y;

    public int X => _x;

    public int Y => _y;

    public Vector2Int(int value) : this(value, value)
    {
    }

    public Vector2Int(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public static Vector2Int Zero => default;
    
    public double Length => Math.Sqrt(LengthSquared);
    
    public long LengthSquared =>_x * (long)_x + _y * (long)_y;
    
    public static Vector2Int operator *(Vector2Int left, Vector2Int right)
    {
        return new Vector2Int(
            left._x * right._x,
            left._y * right._y
        );
    }
    
    public static Vector2Int operator *(Vector2Int left, int right)
    {
        return left * new Vector2Int(right);
    }
    
    public static Vector2Int operator *(int left, Vector2Int right)
    {
        return right * left;
    }

    public static Vector2Int operator /(Vector2Int left, int right)
    {
        return new Vector2Int(left._x / right, left._y / right);
    }
    
    public static Vector2Int operator /(int left, Vector2Int right)
    {
        return new Vector2Int(left / right._x, left / right._y);
    }
    
    public static Vector2Int operator /(Vector2Int left, Vector2Int right)
    {
        return new Vector2Int(left._x / right._x, left._y / right._y);
    }

    public static Vector2Int operator +(Vector2Int left, Vector2Int right)
    {
        return new Vector2Int(
            left._x + right._x,
            left._y + right._y
        );
    }
    
    public static Vector2Int operator -(Vector2Int left, Vector2Int right)
    {
        return new Vector2Int(
            left._x - right._x,
            left._y - right._y
        );
    }
    
    public static Vector2Int operator -(Vector2Int value)
    {
        return Zero - value;
    }
    
    public static bool operator ==(Vector2Int left, Vector2Int right)
    {
        return (left._x == right._x)
               && (left._y == right._y);
    }
    
    public static bool operator !=(Vector2Int left, Vector2Int right)
    {
        return !(left == right);
    }

    public Vector2Int Clamp(int min, int max)
    {
        return new Vector2Int(Math.Clamp(_x, min, max), Math.Clamp(_y, min, max));
    }
    
    public Vector2Int Rotate(float angleInDegrees)
    {
        float angleRadians = angleInDegrees * MathF.PI / 180;
        float cosTheta = MathF.Cos(angleRadians);
        float sinTheta = MathF.Sin(angleRadians);
        
        return new Vector2Int(
            (int)MathF.Round(cosTheta * _x - sinTheta * _y),
            (int)MathF.Round(sinTheta * _x + cosTheta * _y)
        );
    }

    public bool Equals(Vector2Int other)
    {
        return _x == other._x && _y == other._y;
    }

    public override bool Equals(object? obj)
    {
        return obj is Vector2Int other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_x, _y);
    }
    
    public readonly override string ToString()
    {
        return ToString("G", CultureInfo.CurrentCulture);
    }

    public readonly string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format)
    {
        return ToString(format, CultureInfo.CurrentCulture);
    }
    
    public readonly string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format, IFormatProvider? formatProvider)
    {
        string separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

        return $"<{_x.ToString(format, formatProvider)}{separator} {_y.ToString(format, formatProvider)}>";
    }
}