using System;
using System.Collections.Generic;
using System.Linq;
using static Souko.GameDefine;

namespace Souko
{
    internal class Program
    {

        // 現在地のプレイヤーの位置.
        private static int playerPos;

        // 初期値のゴールの位置
        private static List<int> goals = new List<int>();

        private static Dir moveDir;

        // 読み込んだマップ状態.
        private static State[] mapStatus;

        // エントリポイント.
        static void Main(string[] args)
        {
            var fail = Initialize(mapData);
            if (fail)
            {
                Console.WriteLine("不正データの発見.");
                Console.ReadKey();
                return;
            }
            
            // メインループ.
            while (true)
            {
                CursorReset();
                DrawMap(mapStatus);

                // ゴール判定.
                if (IsGameEnd())
                {
                    break;
                }

                // 入力.
                var key = GetKey();
                
                // リセット.
                if (key == ConsoleKey.R)
                {
                    Initialize(mapData);
                }
                
                // 他のキー.
                if (keyToDirTable.ContainsKey(key))
                {
                    // プレイヤーの移動方向.
                    moveDir = keyToDirTable[key];
                    
                    // 不正移動先判定.
                    var nextPosition = GetPlayerNextPosition(moveDir);
                    bool isValidState = CheckValidState(nextPosition, invalidStateTable1);
                    if (!isValidState) continue;

                    // 移動適用.
                    ApplyNextPosition(playerPos, nextPosition, moveDir);
                }
            }

            Console.WriteLine("\n===おわり===\n");
            Console.ReadKey();
        }

        private static bool IsGameEnd()
        {
            foreach (var g in goals)
            {
                var nowState = mapStatus[g];
                if (nowState != State.Stone)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 初期化。マップ読み込み等.
        /// </summary>
        /// <param name="mapData"></param>
        /// <returns>false:正常 true:異常</returns>
        private static bool Initialize(int[]mapData)
        {
            // マップ読み込み.
            mapStatus = LoadMap(mapData);
            
            // プレイヤー位置.
            playerPos = GetStatePosition(mapStatus, State.Player);
            if (playerPos == InvalidIndex)
            {
                Console.WriteLine("マップデータにプレイヤーが見つかりませんでした。");
                return true;
            }
            
            // ゴール位置.
            goals = GetStatePositions(mapStatus, State.Goal);
            if (goals.Count == 0)
            {
                Console.WriteLine("マップデータにゴールが見つかりませんでした。");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 入力取得.
        /// </summary>
        /// <returns></returns>
        private static ConsoleKey GetKey()
        {
            Console.Write("\n");
            return Console.ReadKey().Key;
        }
        
        /// <summary>
        /// 入力チェック.
        /// </summary>
        /// <returns>true:continueする. false:continueしない. </returns>
        private static bool CheckValidInput(ConsoleKey key)
        {

            return false;
        }
        

        /// <summary>
        /// 移動を適用.
        /// </summary>
        /// <param name="nowPosition"></param>
        /// <param name="nextPosition"></param>
        /// <param name="dir"></param>
        private static void ApplyNextPosition(int nowPosition, int nextPosition, Dir dir)
        {
            // 進行方向1マス先に石があるか.
            var existStone = mapStatus[nextPosition] == State.Stone;
            if (existStone)
            {
                // 石が動かせる位置にあるか.
                var canStoneMove = CanStoneMove(nextPosition, dir);
                if (canStoneMove)
                {
                    // 動かせる.
                    // 石の位置を更新.
                    var stoneNextPositino = nextPosition + dirToMoveIndex[(int) dir];
                    UpdateStatus(nextPosition, stoneNextPositino, State.Stone);
                }
                else
                {
                    // 動かせない.
                    // プレイヤーの位置を修正する.
                    nextPosition = nowPosition;
                }
            }

            // プレイヤーの位置を更新.
            UpdateStatus(nowPosition, nextPosition, State.Player);
            playerPos = nextPosition;

            // ゴールの位置を復活
            // Noneということはそのマスには誰もいない.
            foreach (var g in goals)
            {
                if (mapStatus[g] == State.None)
                {
                    UpdateStatus(g, g, State.Goal);
                }
            }
        }

        /// <summary>
        /// 状態を更新.
        /// </summary>
        /// <param name="nowPosition"></param>
        /// <param name="nextPosition"></param>
        /// <param name="state"></param>
        private static void UpdateStatus(int nowPosition, int nextPosition, State state)
        {
            mapStatus[nowPosition] = State.None;
            mapStatus[nextPosition] = state;
        }

        /// <summary>
        /// 石の動かせるか.
        /// </summary>
        /// <param name="nextPosition"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        private static bool CanStoneMove(int nextPosition, Dir dir)
        {
            // プレイヤーの移動先に石があるか.
            if (mapStatus[nextPosition] == State.Stone)
            {
                // 石の移動先が壁や石がなければ動かせる.
                var tmpPos = nextPosition + dirToMoveIndex[(int) dir];
                var isValidState = CheckValidState(tmpPos, invalidStateTable2);
                if (isValidState)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// マップデータから読み込み.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static State[] LoadMap(int[] data)
        {
            State[] ret = new State[data.Length];
            for (int i = 0; i < mapData.Length; ++i)
            {
                ret[i] = (State) mapData[i];
            }

            return ret;
        }

        /// <summary>
        /// ある状態の座標をA取得.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        private static int GetStatePosition(State[] map, State state)
        {
            var i = 0;
            for (i = 0; i < map.Length; i++)
            {
                if (map[i] == state)
                {
                    return i;
                }
            }

            Console.WriteLine($"指定した状態がマップ上に存在しません.\nstate:{state}");
            return -1;
        }
        
        /// <summary>
        /// ある状態の座標をA取得.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        private static List<int> GetStatePositions(State[] map, State state)
        {
            var ret = new List<int>();
            var i = 0;
            for (i = 0; i < map.Length; i++)
            {
                if (map[i] == state)
                {
                    ret.Add(i);
                }
            }

            if(ret.Count == 0)
            {
                Console.WriteLine($"指定した状態がマップ上に存在しません.\nstate:{state}");
            }
            return ret;
        }
        

        /// <summary>
        /// 移動先が正常な状態か.
        /// </summary>
        /// <param name="nextPosition"></param>
        /// <param name="invalidStateTable"></param>
        /// <returns></returns>
        private static bool CheckValidState(int nextPosition, State[] invalidStateTable)
        {
            // マップ外.
            var isValidMapRange = 0 <= nextPosition && nextPosition < mapData.Length;
            if (!isValidMapRange)
            {
                return false;
            }

            // 移動先が有効な状態か.
            var state = mapStatus[nextPosition];
            bool isInvalidState = invalidStateTable.Any(x => x == state);
            if (isInvalidState)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// プレイヤーの移動先座標を取得.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private static int GetPlayerNextPosition(Dir dir)
        {
            //Console.Write(dir);
            return playerPos + dirToMoveIndex[(int) dir];
        }

        /// <summary>
        /// マップ描画.
        /// </summary>
        /// <param name="status"></param>
        private static void DrawMap(State[] status)
        {
            for (int i = 0; i < status.Length; ++i)
            {
                if (i % MapLength == 0)
                {
                    Console.Write("\n");
                }

                var s = stateToIconTable[(int) status[i]];
                Console.Write(s);
            }
        }

        /// <summary>
        /// 描画位置を左上に.
        /// </summary>
        private static void CursorReset()
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
        }
    }
}