using System.Linq;
using Souko.Game.Domain.Map;
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

    /// <summary>
    /// マップをシリアライズしたキャッシュ.
    /// </summary>
    private readonly CacheContainer<int, MapStatus> _cacheContainer = new();

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
        // キャッシュがあるか.
        if (_cacheContainer.Cache.ContainsKey(mapId))
        {
            _status = new MapStatus(_cacheContainer.Cache[mapId]);
            return true;
        }
        
        // シリアライズ.
        var map = _mapDataStore.Load(mapId);
        var status = map.Data.Select(x => (GameDefine.State) x).ToArray();
        _status = new MapStatus(status, map.Width);
        
        // キャッシュする.
        if (_status != null)
        {
            _cacheContainer.Add(mapId, new MapStatus(_status));
            return true;
        }
        
        return false;
    }

    /// <summary>
    /// マップを破棄.
    /// </summary>
    /// <param name="mapId"></param>
    public void Unload(int mapId)
    {
        _cacheContainer.Clear(mapId);
    }

    public void Dispose()
    {
        foreach (var c in _cacheContainer.Cache)
        {
            c.Value.Dispose();
        }
    }
}