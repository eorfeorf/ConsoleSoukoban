using System;
using System.Collections.Generic;

namespace Souko.Game.Domain;

public static class GameDefine
{
    // マップ１辺の長さ
    public const int MapLength = 10;

    // 不正なマップのインデックス.
    public const int InvalidIndex = -1;
        
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
    public static readonly int[] DirToMoveIndex =
    {
        0,
        -MapLength,
        1,
        MapLength,
        -1,
    };

    // タイプによるアイコンテーブル.
    public static readonly string[] StateToIconTable =
    {
        "　",
        "壁",
        "石",
        "〇",
        "ロ",
    };
}