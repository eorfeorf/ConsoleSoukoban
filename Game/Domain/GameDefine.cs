using System;
using System.Collections.Generic;

namespace Souko
{
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

        // キーによる方向テーブル.
        public static readonly Dictionary<ConsoleKey, Dir> KeyToDirTable = new Dictionary<ConsoleKey, Dir>()
        {
            {ConsoleKey.W, Dir.Up},
            {ConsoleKey.A, Dir.Left},
            {ConsoleKey.S, Dir.Down},
            {ConsoleKey.D, Dir.Right},
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
}
