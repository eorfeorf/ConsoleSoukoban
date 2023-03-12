using Souko.Game.Domain.Input;

namespace Souko.Game.Domain.UseCase;

public class InputUseCase
{
    private LoggerUseCase _loggerUseCase;
    private IInputController inputController;

    private InputData inputData;
    
    public InputUseCase(LoggerUseCase loggerUseCase, IInputController inputController)
    {
        _loggerUseCase = loggerUseCase;
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