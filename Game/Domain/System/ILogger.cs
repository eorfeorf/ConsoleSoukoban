namespace Souko.Game.Domain.System;

/// <summary>
/// ログ出力を吸収するインターフェイス.
/// </summary>
public interface ILogger
{
    /// <summary>
    /// 出力.
    /// </summary>
    /// <param name="message"></param>
    public void Log(string message);
}