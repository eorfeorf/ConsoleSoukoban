using System.Collections.Generic;

namespace Souko.Game.Domain.UseCase.Component;

public record OriginalPositionData(Vector2Int player, List<Vector2Int> goals)
{
    public Vector2Int player { get; } = player;
    public List<Vector2Int> goals { get; } = goals;
}