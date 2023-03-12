namespace Souko.Game.Domain.UseCase.Component;

public static class IntExtension
{
    public static Vector2Int ToVec2Int(this int x, int length)
    {
        return new Vector2Int(x % length, x / length);
    }
}