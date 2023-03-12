using System.Linq;
using Souko.Game.Domain;
using Souko.Game.Domain.Map;
using Souko.Game.Domain.UseCase;
using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Data.Map;

/// <summary>
/// マップデータの保持.
/// </summary>
public class MapRepository : IMapRepository
{
    /// <summary>
    /// マップデータのインスタンス.
    /// これがマップの状態.
    /// </summary>
    public MapStatus Status => _status;
    private MapStatus _status;
    
    private readonly IMapDataStore _mapDataStore;
    
    public MapRepository(IMapDataStore mapDataStore)
    {
        _mapDataStore = mapDataStore;
    }

    /// <summary>
    /// 用意されたマップデータのシリアライズ、インスタンス化.
    /// </summary>
    /// <param name="mapId"></param>
    /// <returns></returns>
    public bool Load(int mapId)
    {
        // TODO:キャッシュが欲しい.
        var map = _mapDataStore.Load(mapId);
        _status = new MapStatus(map.Data.Select(x => (GameDefine.State)x).ToArray(), map.Width);
        return _status != null;
    }
}