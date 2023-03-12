using System;
using System.Collections.Generic;
using Souko.Game.Data.Map;
using Souko.Game.Domain;
using Souko.Game.Domain.UseCase;
using Souko.Game.Domain.UseCase.Component;
using Souko.Game.Presentation.Input;
using Souko.Game.Presentation.View;
using Souko.Game.UI.Input;
using Souko.Game.UI.View;
using static Souko.Game.Domain.GameDefine;

namespace Souko.Game
{
    internal static class Game
    {
        // UseCase.
        private static MapUseCase mapUseCase;
        private static InputUseCase inputUseCase;
        private static PlayerUseCase playerUseCase;
        private static InGameFlowUseCase inGameFlowUseCase;
        
        // 初期値のゴールの位置
        private static List<Vector2Int> goals = new();
        
        // エントリポイント.
        static void Main(string[] args)
        {
            DependencyResolve();
            
            // 初期化.
            var fail = Initialize(0);
            if (fail)
            {
                Console.ReadKey();
                return;
            }
            
            // メインループ.
            while (true)
            {
                mapUseCase.Draw();

                // ゴール判定.
                if (inGameFlowUseCase.IsGameEnd(goals))
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
                    var nextPosition = playerUseCase.GetNextPosition(dir);
                    bool isValidState = mapUseCase.CheckValidState(nextPosition, DirToMoveIndex[(int)dir]);
                    if (!isValidState) continue;

                    // 移動適用.
                    ApplyNextPosition(playerUseCase.Pos, nextPosition, DirToMoveIndex[(int)dir]);
                }
            }

            Console.WriteLine("\n===おわり===\n");
            Console.ReadKey();
        }

        private static void DependencyResolve()
        {
            mapUseCase = new MapUseCase(
                new MapRepository(new MapDataStore()),
                new MapView(
                    new CliMapDrawer(new CliDrawer()),
                    new CliMapViewSetupper()
                )
            );
            inputUseCase = new InputUseCase(new InputController(new InputKeyboard()));
            playerUseCase = new PlayerUseCase();
            inGameFlowUseCase = new InGameFlowUseCase(mapUseCase);
        }
        
        /// <summary>
        /// 初期化。マップ読み込み等.
        /// </summary>
        /// <param name="mapId"></param>
        /// <returns>false:正常 true:異常</returns>
        public static bool Initialize(int mapId)
        {
            // マップ読み込み.
            if (!mapUseCase.Load(mapId, out var originalPositionData))
            {
                Console.WriteLine("マップデータが不正でした。");
                return true;
            }

            playerUseCase.Pos = originalPositionData.player;
            goals = originalPositionData.goals;
            return false;
        }
        
        /// <summary>
        /// 移動を適用.
        /// </summary>
        /// <param name="nowPosition"></param>
        /// <param name="nextPosition"></param>
        /// <param name="dir"></param>
        private static void ApplyNextPosition(Vector2Int nowPosition, Vector2Int nextPosition, Vector2Int moveValue)
        {
            #if false
            for (var i = 2; i > 0; --i)
            {
                var nowPos = nowPosition + moveValue * (i - 1);
                var nextPos = nowPosition + moveValue * i;

                if (mapUseCase.Status[nextPos] != State.Wall &&
                    mapUseCase.Status[nextPos] != State.Stone)
                {
                    mapUseCase.UpdateStatus(nowPos, nextPos, mapUseCase.Status[nowPos]);
                }
            }
            
            // ゴールの位置を復活
            // Noneということはそのマスには誰もいない.
            foreach (var g in goals)
            {
                if (mapUseCase.Status[g] == State.None)
                {
                    mapUseCase.UpdateStatus(g, g, State.Goal);
                }
            }

            #else

            // 石の位置を更新.
            if (mapUseCase.Status[nextPosition] == State.Stone)
            {
                var nextPosition2Ahead = nextPosition + moveValue;
                mapUseCase.UpdateStatus(nextPosition, nextPosition2Ahead, State.Stone);
            }

            // プレイヤーの位置を更新.
            mapUseCase.UpdateStatus(nowPosition, nextPosition, State.Player);
            playerUseCase.Pos = nextPosition;

            // ゴールの位置を復活
            // Noneということはそのマスには誰もいない.
            foreach (var g in goals)
            {
                if (mapUseCase.Status[g] == GameDefine.State.None)
                {
                    mapUseCase.UpdateStatus(g, g, GameDefine.State.Goal);
                }
            }
            #endif
        }
    }
}