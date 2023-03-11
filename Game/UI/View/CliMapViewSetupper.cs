using System;
using Souko.Game.Domain.Map;
using Souko.Game.Presentation.View;

namespace Souko.Game.UI.View;

public class CliMapViewSetupper : IMapViewSetupper
{
    public void Setup()
    {
        // 描画位置を左上に.
        Console.CursorLeft = 0;
        Console.CursorTop = 0;
    }
}