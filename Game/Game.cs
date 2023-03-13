using System;
using Souko.Game.Data.Map;
using Souko.Game.Domain.UseCase;
using Souko.Game.Presentation.Input.Keyboard;
using Souko.Game.Presentation.View;
using Souko.Game.UI.Input;
using Souko.Game.UI.System;

namespace Souko.Game
{
    internal static class Game
    {
        // UseCase.
        private static MapUseCase mapUseCase;
        private static InputUseCase inputUseCase;
        private static PlayerUseCase playerUseCase;
        private static LoggerUseCase loggerUseCase;
        private static InGameFrameworkUseCase inGameFrameworkUseCase;
        
        // 依存解決.
        // TODO:DIContainerを使う.
        private static void DependencyResolve()
        {
            loggerUseCase = new LoggerUseCase(new LoggerConsole());
            mapUseCase = new MapUseCase(loggerUseCase, new MapRepository(new MapDataStore()), new MapViewCLI(new MapViewCLIMapper()));
            inputUseCase = new InputUseCase(loggerUseCase, new InputControllerKeyboard(new InputKeyboard(), new InputControllerKeyboardMapper()));
            playerUseCase = new PlayerUseCase(loggerUseCase, mapUseCase);
            inGameFrameworkUseCase = new InGameFrameworkUseCase(loggerUseCase, mapUseCase, inputUseCase, playerUseCase);
        }

        // エントリポイント.
        static void Main(string[] args)
        {
            // 依存解決.
            DependencyResolve();

            // 初期化.
            var success = inGameFrameworkUseCase.Initialize(0);
            if (!success)
            {
                loggerUseCase.Log("初期化の失敗しました。=\n");
                return;
            }

            // メインループ.
            while (true)
            {
                inGameFrameworkUseCase.Draw();
                if (inGameFrameworkUseCase.Update())
                {
                    break;
                }
            }

            loggerUseCase.Log("\n===おわり===\n");

            // コンソール依存なので放置.
            Console.ReadKey();
        }
    }
}