using System;
using Souko.Game.Domain.System;

namespace Souko.Game.UI.System;

/// <summary>
/// コンソールアプリケーションでログ出力を行うクラス.
/// </summary>
public class LoggerConsole : ILogger
{
    public void Log(string message)
    {
        Console.Write(message);
    }
}