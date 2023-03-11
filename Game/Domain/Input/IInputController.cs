namespace Souko.Game.Domain.Input;

public interface IInputController
{
    // public GameDefine.Dir GetDir();
    // public bool GetReset();
    public InputData GetInput();
}