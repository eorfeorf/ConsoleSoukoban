using System;

namespace Souko.Game.Presentation.Input;

public interface IInputDevice
{
    public ConsoleKey GetKey();
}