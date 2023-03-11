using System;
using System.Collections.Generic;
using Souko.Game.Data.Map;
using Souko.Game.Domain;
using Souko.Game.Domain.Map;
using Souko.Game.Domain.UseCase;
using Souko.Game.Presentation.Input;
using Souko.Game.Presentation.View;
using Souko.Game.UI.Input;
using Souko.Game.UI.View;
using static Souko.Game.Domain.GameDefine;

namespace Souko.Game
{
    internal static class Game
    {
        // 現在地のプレイヤーの位置.
        private static int playerPos;

        // 初期値のゴールの位置
        private static List<int> goals = new List<int>();

        // UseCase.
        private static MapUseCase mapUseCase;
        private static InputUseCase inputUseCase;
        
        // エントリポイント.
        static void Main(string[] args)
        {
            // Resolve.
            mapUseCase = new MapUseCase(
                new MapRepository(new MapDataStore()),
                new MapView(new CliMapDrawer(new CliDrawer())));
            inputUseCase = new InputUseCase(new InputController(new InputKeyboard()));
            
            // 初期化.
            var fail = Initialize(0);
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
                mapUseCase.Draw();

                // ゴール判定.
                if (IsGameEnd())
                {
                    break;
                }
                
                // 入力更新.
                inputUseCase.UpdateInput();

                // リセット.
                if (inputUseCase.GetReset())
                {
                    Initialize(0);
                }
                
                // プレイヤー移動.
                var dir = inputUseCase.GetDir();
                if (dir != Dir.None)
                {
                    // 不正移動先判定.
                    var nextPosition = GetPlayerNextPosition(dir);
                    bool isValidState = CheckValidState(nextPosition, DirToMoveIndex[(int)dir]);
                    if (!isValidState) continue;

                    // 移動適用.
                    ApplyNextPosition(playerPos, nextPosition, DirToMoveIndex[(int)dir]);
                }
            }

            Console.WriteLine("\n===おわり===\n");
            Console.ReadKey();
        }

        /// <summary>
        /// 終了判定.
        /// </summary>
        /// <returns></returns>
        private static bool IsGameEnd()
        {
            foreach (var g in goals)
            {
                var nowState = mapUseCase.Status[g];
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
        private static bool Initialize(int mapId)
        {
            // マップ読み込み.
            if (!mapUseCase.Load(mapId))
            {
                Console.WriteLine("マップデータが存在しません。");
            }
            
            // プレイヤー位置.
            playerPos = mapUseCase.GetStatePosition(State.Player);
            if (playerPos == InvalidIndex)
            {
                Console.WriteLine("マップデータにプレイヤーが見つかりませんでした。");
                return true;
            }
            
            // ゴール位置.
            goals = mapUseCase.GetStatePositions(State.Goal);
            if (goals.Count == 0)
            {
                Console.WriteLine("マップデータにゴールが見つかりませんでした。");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 移動を適用.
        /// </summary>
        /// <param name="nowPosition"></param>
        /// <param name="nextPosition"></param>
        /// <param name="dir"></param>
        private static void ApplyNextPosition(int nowPosition, int nextPosition, int moveValue)
        {
            // 石の位置を更新.
            if (mapUseCase.Status[nextPosition] == State.Stone)
            {
                var nextPosition2Ahead = nextPosition + moveValue;
                mapUseCase.UpdateStatus(nextPosition, nextPosition2Ahead, State.Stone);
            }

            // プレイヤーの位置を更新.
            mapUseCase.UpdateStatus(nowPosition, nextPosition, State.Player);
            playerPos = nextPosition;

            // ゴールの位置を復活
            // Noneということはそのマスには誰もいない.
            foreach (var g in goals)
            {
                if (mapUseCase.Status[g] == State.None)
                {
                    mapUseCase.UpdateStatus(g, g, State.Goal);
                }
            }
        }
        
        /// <summary>
        /// 移動先が正常な状態か.
        /// </summary>
        /// <param name="nextPosition"></param>
        /// <param name="moveValue"></param>
        /// <returns></returns>
        private static bool CheckValidState(int nextPosition, int moveValue)
        {
            // マップ外.
            var isValidMapRange = 0 <= nextPosition && nextPosition < mapUseCase.Status.Length;
            if (!isValidMapRange)
            {
                return false;
            }

            // 移動先が有効な状態か.
            var state = mapUseCase.Status[nextPosition];
            
            // 壁.
            if (state == State.Wall)
            {
                return false;
            }
            
            // 石.
            if (state == State.Stone)
            {
                // 石の先が移動できるか.
                var nextPosition2Ahead = nextPosition + moveValue;
                var state2 = mapUseCase.Status[nextPosition2Ahead];
                if (state2 == State.Wall || state2 == State.Stone)
                {
                    return false;
                }
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
            return playerPos + DirToMoveIndex[(int) dir];
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