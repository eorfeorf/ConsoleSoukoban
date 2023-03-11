namespace Souko.Game.Domain.Map;

public interface IMapRepository
{
    public int[] Load(int mapId);
}