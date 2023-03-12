namespace Souko.Game.Domain.UseCase.Component;

public struct Vector2Int
{
    public int x;
    public int y;

    public Vector2Int(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
    public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new Vector2Int(a.x + b.x, a.y + b.y);
    public static Vector2Int operator -(Vector2Int v) => new Vector2Int(-v.x, -v.y);
    public static Vector2Int operator -(Vector2Int a, Vector2Int b) => new Vector2Int(a.x - b.x, a.y - b.y);
    public static Vector2Int operator *(Vector2Int a, Vector2Int b) => new Vector2Int(a.x * b.x, a.y * b.y);
    public static Vector2Int operator *(int a, Vector2Int b) => new Vector2Int(a * b.x, a * b.y);
    public static Vector2Int operator *(Vector2Int a, int b) => new Vector2Int(a.x * b, a.y * b);
    public static Vector2Int operator /(Vector2Int a, int b) => new Vector2Int(a.x / b, a.y / b);
    public static bool operator ==(Vector2Int lhs, Vector2Int rhs) => lhs.x == rhs.x && lhs.y == rhs.y;
    public static bool operator !=(Vector2Int lhs, Vector2Int rhs) => !(lhs == rhs);
    
    public bool Equals(Vector2Int other)
    {
        return x == other.x && y == other.y;
    }

    public override bool Equals(object obj)
    {
        return obj is Vector2Int other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (x * 397) ^ y;
        }
    }
}