using System;
using Souko.Game.Presentation.Input;

namespace Souko.Game.UI.Input;

/// <summary>
/// キーボードの入力を取得するクラス.
/// </summary>
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