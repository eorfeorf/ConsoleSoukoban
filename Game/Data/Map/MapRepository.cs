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
    private readonly IMapDataStore mapDataStore;

    public MapStatus Status => status;
    private MapStatus status;
    
    public MapRepository(IMapDataStore mapDataStore)
    {
        this.mapDataStore = mapDataStore;
    }

    public bool Load(int mapId)
    {
        status = new MapStatus(mapDataStore.Load(mapId).Select(x => (GameDefine.State)x).ToArray(), GameDefine.MapLength);
        return status != null;
    }
}