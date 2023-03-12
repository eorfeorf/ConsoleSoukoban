namespace Souko.Game.Domain.UseCase.Component;

public class PlayerUseCase
{
    // 現在地のプレイヤーの位置.
    public Vector2Int Pos;

    private MapUseCase _mapUseCase;

    public PlayerUseCase(MapUseCase mapUseCase)
    {
        _mapUseCase = mapUseCase;
    }

    /// <summary>
    /// プレイヤーの移動先座標を取得.
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public Vector2Int GetNextPosition(GameDefine.Dir dir)
    {
        var index = (int) dir;
        return Pos + GameDefine.DirToMoveIndex[index];
    }
    
    /// <summary>
    /// 移動を適用.
    /// </summary>
    /// <param name="nowPosition"></param>
    /// <param name="nextPosition"></param>
    /// <param name="dir"></param>
    public void ApplyNextPosition(Vector2Int nowPosition, Vector2Int nextPosition, Vector2Int moveValue)
    {
        // 石の位置を更新.
        if (_mapUseCase.Status[nextPosition] == GameDefine.State.Stone)
        {
            var nextPosition2Ahead = nextPosition + moveValue;
            _mapUseCase.UpdateStatus(nextPosition, nextPosition2Ahead, GameDefine.State.Stone);
        }

        // プレイヤーの位置を更新.
        _mapUseCase.UpdateStatus(nowPosition, nextPosition, GameDefine.State.Player);
        Pos = nextPosition;

        // ゴールの位置を復活
        // Noneということはそのマスには誰もいない.
        foreach (var g in _mapUseCase.OriginalGoalPos)
        {
            if (_mapUseCase.Status[g] == GameDefine.State.None)
            {
                _mapUseCase.UpdateStatus(g, g, GameDefine.State.Goal);
            }
        }
    }
}