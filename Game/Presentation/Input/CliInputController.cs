using System;
using System.Collections.Generic;
using Souko.Game.Domain;
using Souko.Game.Domain.Input;

namespace Souko.Game.Presentation.Input;

public class CliInputController : IInputController
{
    private IInputDevice inputDevice;

    // キーボードによる方向テーブル.
    private readonly Dictionary<ConsoleKey, GameDefine.Dir> keyboardToDirTable = new()
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
    
    public CliInputController(IInputDevice inputDevice)
    {
        this.inputDevice = inputDevice;
    }

    public InputData GetInput()
    {
        var key = inputDevice.GetKey();
        var data = new InputData();

        // Dir.
        data.Dir = keyboardToDirTable.ContainsKey(key) ? keyboardToDirTable[key] : GameDefine.Dir.None;

        // Reset.
        data.Reset = key == ConsoleKey.R;

        return data;
    }
}