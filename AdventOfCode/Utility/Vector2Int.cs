using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace AdventOfCode.Utility;

public readonly struct Vector2Int : IEquatable<Vector2Int>, IFormattable
{
    public int X { get; }

    public int Y { get; }
    
    public Vector2Int(int value) : this(value, value)
    {
    }

    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Vector2Int Zero => default;
    
    public static Vector2Int operator *(Vector2Int left, Vector2Int right)
    {
        return new Vector2Int(
            left.X * right.X,
            left.Y * right.Y
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
    
    public static Vector2Int operator +(Vector2Int left, Vector2Int right)
    {
        return new Vector2Int(
            left.X + right.X,
            left.Y + right.Y
        );
    }
    
    public static Vector2Int operator -(Vector2Int left, Vector2Int right)
    {
        return new Vector2Int(
            left.X - right.X,
            left.Y - right.Y
        );
    }
    
    public static Vector2Int operator -(Vector2Int value)
    {
        return Zero - value;
    }
    
    public static bool operator ==(Vector2Int left, Vector2Int right)
    {
        return (left.X == right.X)
               && (left.Y == right.Y);
    }
    
    public static bool operator !=(Vector2Int left, Vector2Int right)
    {
        return !(left == right);
    }

    public bool Equals(Vector2Int other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is Vector2Int other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
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

        return $"<{X.ToString(format, formatProvider)}{separator} {Y.ToString(format, formatProvider)}>";
    }
}