using Souko.Game.Domain.Map;

namespace Souko.Game.Data.Map;

/// <summary>
/// マップ
/// </summary>
public class MapRepository : IMapRepository
{
    private readonly IMapDataStore mapDataStore;

    public MapRepository(IMapDataStore mapDataStore)
    {
        this.mapDataStore = mapDataStore;
    }
        
    public int[] Load(int mapId)
    {
        return mapDataStore.Load(mapId);
    }
}