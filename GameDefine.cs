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
        
        // マップデータ.
        public static int[] mapData =
        {
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 3, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 2, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 2, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
            1, 0, 1, 0, 0, 1, 1, 1, 0, 1,
            1, 0, 1, 4, 0, 4, 0, 0, 0, 1,
            1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
        };

        
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
        public static int[] dirToMoveIndex =
        {
            0,
            -10,
            1,
            10,
            -1,
        };

        // キーによる方向テーブル.
        public static Dictionary<ConsoleKey, Dir> keyToDirTable = new Dictionary<ConsoleKey, Dir>()
        {
            {ConsoleKey.W, Dir.Up},
            {ConsoleKey.A, Dir.Left},
            {ConsoleKey.S, Dir.Down},
            {ConsoleKey.D, Dir.Right},
        };

        // タイプによるアイコンテーブル.
        public static string[] stateToIconTable =
        {
            "　",
            "壁",
            "石",
            "〇",
            "ロ",
        };

        // １コマ先で移動不可能な状態テーブル.
        public static State[] invalidStateTable1 =
        {
            State.Wall,
        };

        // ２コマ先で移動不可能な状態テーブル.
        public static State[] invalidStateTable2 =
        {
            State.Wall,
            State.Stone,
        };
    }
}
