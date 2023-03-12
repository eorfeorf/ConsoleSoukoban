namespace Souko.Game.Domain.Input;

/// <summary>
/// 入力情報を抽象化したデータ.
/// </summary>
/// <param name="Dir">プレイヤーの入力方向.</param>
/// <param name="Reset">リセット.</param>
public record struct InputData(GameDefine.Dir Dir, bool Reset);
