using Souko.Game.Domain.Input;
using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Domain.UseCase;

/// <summary>
/// 入力を制御するユースケース.
/// </summary>
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

    /// <summary>
    /// 入力更新.
    /// </summary>
    public void UpdateInput()
    {
        _inputData = _inputController.GetInput();
    }
    
    /// <summary>
    /// 移動方向取得.
    /// </summary>
    /// <returns></returns>
    public GameDefine.Dir GetDir()
    {
        return _inputData.Dir;
    }
    
    /// <summary>
    /// リセット取得.
    /// </summary>
    /// <returns></returns>
    public bool GetReset()
    {
        return _inputData.Reset;
    }
}