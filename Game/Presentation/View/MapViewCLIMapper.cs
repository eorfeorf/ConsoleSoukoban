using System.Collections.Generic;
using Souko.Game.Domain;
using Souko.Game.Domain.UseCase;
using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Presentation.View;

/// <summary>
/// マップの状態と表示を紐づけるクラス.
/// </summary>
public class MapViewCLIMapper
{
    /// <summary>
    /// 状態と表示を紐づけるテーブル.
    /// </summary>
    public readonly Dictionary<GameDefine.State, string> StateToIconTable = new()
    {
        {GameDefine.State.None, "　"},
        {GameDefine.State.Wall, "壁"},
        {GameDefine.State.Stone, "石"},
        {GameDefine.State.Player, "〇"},
        {GameDefine.State.Goal, "ロ"}
    };
}