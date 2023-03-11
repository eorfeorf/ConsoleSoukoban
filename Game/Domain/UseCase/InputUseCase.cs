using Souko.Game.Domain.Input;

namespace Souko.Game.Domain.UseCase;

public class InputUseCase
{
    private IInputController inputController;

    private InputData inputData;
    
    public InputUseCase(IInputController inputController)
    {
        this.inputController = inputController;
    }

    public void UpdateInput()
    {
        inputData = inputController.GetInput();
    }
    
    
    public GameDefine.Dir GetDir()
    {
        return inputData.Dir;
    }
    
    public bool GetReset()
    {
        return inputData.Reset;
    }
}