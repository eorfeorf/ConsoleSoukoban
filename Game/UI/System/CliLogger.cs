using System;
using Souko.Game.Domain.System;

namespace Souko.Game.UI.System;

public class CliLogger : ILogger
{
    public void Log(string message)
    {
        Console.Write(message);
    }
}