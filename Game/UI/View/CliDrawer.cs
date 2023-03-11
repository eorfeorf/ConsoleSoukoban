using System;
using Souko.Game.Presentation.View;

namespace Souko.Game.UI.View;

public class CliDrawer : IDrawer
{
    public void Draw(string s)
    {
        Console.Write(s);
    }
}