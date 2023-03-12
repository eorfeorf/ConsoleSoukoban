using System;
using System.Collections.Generic;
using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Domain;

public static class GameDefine
{
    // マップ１辺の長さ
    public const int MapLength = 10;

    // 不正なマップのインデックス.
    public const int InvalidIndex = -1;
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
    public static readonly Vector2Int[] DirToMoveIndex =
    {
        new(0,0),
        new(0, -1),
        new(1, 0),
        new(0, 1),
        new(-1,0),
    };
}