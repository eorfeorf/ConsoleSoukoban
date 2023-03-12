using System.Collections.Generic;

namespace Souko.Game.Domain.UseCase.Component;

public static class GameDefine
{
    // マップ１辺の長さ
    // TODO:マップデータから取得する.
    public const int MapLength = 10;

    // 不正なマップのインデックス.
    public static readonly Vector2Int InvalidPos = new Vector2Int(-1, -1);

    // 入力を抽象化する方向.
    public enum Dir
    {
        None,
        Up,
        Right,
        Down,
        Left,
    }

    // マップの状態.
    public enum State
    {
        None,
        Wall,
        Stone,
        Player,
        Goal,
    };

    // 方向による移動量テーブル.
    public static readonly Dictionary<Dir, Vector2Int> DirToMoveValue = new()
    {
        {Dir.None,  new( 0,  0)},
        {Dir.Up,    new( 0, -1)},
        {Dir.Right, new( 1,  0)},
        {Dir.Down,  new( 0,  1)},
        {Dir.Left,  new(-1,  0)},
    };
}