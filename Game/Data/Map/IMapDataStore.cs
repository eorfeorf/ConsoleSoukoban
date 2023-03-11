namespace Souko.Game.Data.Map;

public interface IMapDataStore
{
    public int[] Load(int mapId);
}