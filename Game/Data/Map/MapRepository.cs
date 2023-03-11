using System.Linq;
using Souko.Game.Domain;
using Souko.Game.Domain.Map;

namespace Souko.Game.Data.Map;

/// <summary>
/// マップ
/// </summary>
public class MapRepository : IMapRepository
{
    private readonly IMapDataStore mapDataStore;

    public GameDefine.State[] Status => status;
    private GameDefine.State[] status;
    
    public MapRepository(IMapDataStore mapDataStore)
    {
        this.mapDataStore = mapDataStore;
    }

    public bool Load(int mapId)
    {
        status = mapDataStore.Load(mapId).Select(x => (GameDefine.State)x).ToArray();
        return status != null;
    }
}