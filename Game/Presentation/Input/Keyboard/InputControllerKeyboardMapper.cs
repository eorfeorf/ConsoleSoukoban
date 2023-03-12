using System;
using System.Collections.Generic;
using Souko.Game.Domain;
using Souko.Game.Domain.UseCase;
using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Presentation.Input.Keyboard;

/// <summary>
/// キーボード入力と操作を紐づけるクラス.
/// </summary>
public class InputControllerKeyboardMapper
{
    /// <summary>
    /// キー入力と方向を紐づけるテーブル.
    /// </summary>
    public readonly Dictionary<ConsoleKey, GameDefine.Dir> KeyboardToDirTable = new()
    {
        {ConsoleKey.W, GameDefine.Dir.Up},
        {ConsoleKey.A, GameDefine.Dir.Left},
        {ConsoleKey.S, GameDefine.Dir.Down},
        {ConsoleKey.D, GameDefine.Dir.Right},
        {ConsoleKey.UpArrow, GameDefine.Dir.Up},
        {ConsoleKey.LeftArrow, GameDefine.Dir.Left},
        {ConsoleKey.DownArrow, GameDefine.Dir.Down},
        {ConsoleKey.RightArrow, GameDefine.Dir.Right},
    };
}