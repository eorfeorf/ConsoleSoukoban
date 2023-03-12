using System;
using Souko.Game.Domain;
using Souko.Game.Domain.Input;
using Souko.Game.Domain.UseCase;
using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Presentation.Input.Keyboard;

/// <summary>
/// キーボードによる入力制御.
/// </summary>
public class InputControllerKeyboard : IInputController
{
    private readonly IInputDevice _inputDevice;
    private readonly InputControllerKeyboardMapper _inputControllerKeyboardMapper;
    
    public InputControllerKeyboard(IInputDevice inputDevice, InputControllerKeyboardMapper inputControllerKeyboardMapper)
    {
        _inputDevice = inputDevice;
        _inputControllerKeyboardMapper = inputControllerKeyboardMapper;
    }

    /// <summary>
    /// 入力取得.
    /// </summary>
    /// <returns></returns>
    public InputData GetInput()
    {
        Console.Write("\n");
        var key = _inputDevice.GetKey();
        var data = new InputData();

        // Dir.
        data.Dir = _inputControllerKeyboardMapper.KeyboardToDirTable.ContainsKey(key)
            ?_inputControllerKeyboardMapper.KeyboardToDirTable[key]
            : GameDefine.Dir.None;

        // Reset.
        data.Reset = key == ConsoleKey.R;

        return data;
    }
}