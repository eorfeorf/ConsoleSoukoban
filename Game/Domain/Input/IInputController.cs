namespace Souko.Game.Domain.Input;

/// <summary>
/// 入力情報を抽象化するインターフェイス.
/// </summary>
public interface IInputController
{
    /// <summary>
    /// 抽象化した入力を取得.
    /// </summary>
    /// <returns></returns>
    public InputData GetInput();
}