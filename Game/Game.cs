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
        private static GameFlowUseCase gameFlowUseCase;
        
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
                if (gameFlowUseCase.IsGameEnd(mapUseCase.OriginalGoalPos))
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
                    playerUseCase.ApplyNextPosition(playerUseCase.Pos, nextPosition, DirToMoveIndex[(int)dir]);
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
            playerUseCase = new PlayerUseCase(mapUseCase);
            gameFlowUseCase = new GameFlowUseCase(mapUseCase);
        }
        
        /// <summary>
        /// 初期化。マップ読み込み等.
        /// </summary>
        /// <param name="mapId"></param>
        /// <returns>false:正常 true:異常</returns>
        public static bool Initialize(int mapId)
        {
            // マップ読み込み.
            if (!mapUseCase.Load(mapId))
            {
                Console.WriteLine("マップデータが不正でした。");
                return true;
            }

            playerUseCase.Pos = mapUseCase.OriginalPlayerPos;
            return false;
        }
        
    }
}