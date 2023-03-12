using System;
using Souko.Game.Data.Map;
using Souko.Game.Domain.UseCase;
using Souko.Game.Domain.UseCase.Component;
using Souko.Game.Presentation.Input;
using Souko.Game.Presentation.View;
using Souko.Game.UI.Input;
using Souko.Game.UI.System;
using Souko.Game.UI.View;

namespace Souko.Game
{
    internal static class Game
    {
        // UseCase.
        private static MapUseCase mapUseCase;
        private static InputUseCase inputUseCase;
        private static PlayerUseCase playerUseCase;
        private static GameFlowUseCase gameFlowUseCase;
        private static LoggerUseCase loggerUseCase;
        private static InGameFrameworkUseCase inGameFrameworkUseCase;
        
        // エントリポイント.
        static void Main(string[] args)
        {
            DependencyResolve();
            
            // 初期化.
            var success = inGameFrameworkUseCase.Initialize(0);
            if (!success)
            {
                loggerUseCase.Log("初期化の失敗しました。=\n");
                return;
            }
            
            // メインループ.
            while (inGameFrameworkUseCase.IsEnd())
            {
                inGameFrameworkUseCase.Draw();
                inGameFrameworkUseCase.Update();
            }

            loggerUseCase.Log("\n===おわり===\n");
            Console.ReadKey();
        }

        private static void DependencyResolve()
        {
            loggerUseCase = new LoggerUseCase(new CliLogger());
            mapUseCase = new MapUseCase(
                loggerUseCase,
                new MapRepository(new MapDataStore()),
                new MapView(
                    new CliMapDrawer(new CliDrawer()),
                    new CliMapViewSetupper()
                )
            );
            inputUseCase = new InputUseCase(loggerUseCase, new InputController(new InputKeyboard()));
            playerUseCase = new PlayerUseCase(loggerUseCase, mapUseCase);
            gameFlowUseCase = new GameFlowUseCase(loggerUseCase, mapUseCase);
            inGameFrameworkUseCase = new InGameFrameworkUseCase(loggerUseCase, mapUseCase, inputUseCase, playerUseCase, gameFlowUseCase);
        }
    }
}