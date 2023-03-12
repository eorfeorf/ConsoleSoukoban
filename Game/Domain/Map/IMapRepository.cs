using System;
using Souko.Game.Data;
using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Domain.Map;

/// <summary>
/// マップの読み込み、保持を吸収するインターフェイス.
/// TODO:マップ読み込みを分離したほうがいいかも.
/// </summary>
public interface IMapRepository : IDisposable
{
    /// <summary>
    /// マップのインスタンス.
    /// </summary>
    public MapStatus Status { get; }
    
    /// <summary>
    /// マップ読み込み.
    /// </summary>
    /// <param name="mapId"></param>
    /// <returns></returns>
    public bool Load(int mapId);

    /// <summary>
    /// マップ破棄.
    /// </summary>
    /// <param name="mapId"></param>
    public void Unload(int mapId);
}