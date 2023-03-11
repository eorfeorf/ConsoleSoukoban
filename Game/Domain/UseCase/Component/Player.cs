namespace Souko.Game.Domain.UseCase.Component;

public class Player
{
    // 現在地のプレイヤーの位置.
    public int Pos;

    /// <summary>
    /// プレイヤーの移動先座標を取得.
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public int GetNextPosition(GameDefine.Dir dir)
    {
        return Pos + GameDefine.DirToMoveIndex[(int) dir];
    }
}