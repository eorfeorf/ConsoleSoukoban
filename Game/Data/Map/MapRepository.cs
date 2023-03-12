using System.Linq;
using Souko.Game.Domain;
using Souko.Game.Domain.Map;
using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Data.Map;

/// <summary>
/// マップ
/// </summary>
public class MapRepository : IMapRepository
{
    public MapStatus Status => _status;
    private MapStatus _status;
    
    private readonly IMapDataStore _mapDataStore;
    
    public MapRepository(IMapDataStore mapDataStore)
    {
        _mapDataStore = mapDataStore;
    }

    public bool Load(int mapId)
    {
        _status = new MapStatus(_mapDataStore.Load(mapId).Select(x => (GameDefine.State)x).ToArray(), GameDefine.MapLength);
        return _status != null;
    }
}