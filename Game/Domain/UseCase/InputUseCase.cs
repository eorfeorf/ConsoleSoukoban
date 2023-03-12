using Souko.Game.Domain.Input;

namespace Souko.Game.Domain.UseCase;

public class InputUseCase
{
    private readonly LoggerUseCase _loggerUseCase;
    private readonly IInputController _inputController;

    private InputData _inputData;
    
    public InputUseCase(LoggerUseCase loggerUseCase, IInputController inputController)
    {
        _loggerUseCase = loggerUseCase;
        _inputController = inputController;
    }

    public void UpdateInput()
    {
        _inputData = _inputController.GetInput();
    }
    
    public GameDefine.Dir GetDir()
    {
        return _inputData.Dir;
    }
    
    public bool GetReset()
    {
        return _inputData.Reset;
    }
}