namespace Souko.Game.Domain.Map;

public interface IMapDataStore
{
    public int[] Load(int mapId);
}