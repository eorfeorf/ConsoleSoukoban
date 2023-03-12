namespace Souko.Game.Domain.UseCase.Component;

public class PlayerUseCase
{
    // 現在地のプレイヤーの位置.
    public Vector2Int Pos;

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
}