using System;
using Souko.Game.Presentation.Input;

namespace Souko.Game.UI.Input;

public class InputKeyboard : IInputDevice
{
    /// <summary>
    /// 入力取得.
    /// </summary>
    /// <returns></returns>
    public ConsoleKey GetKey()
    {
        return Console.ReadKey().Key;
    }
}