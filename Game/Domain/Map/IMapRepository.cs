namespace Souko.Game.Domain.Map;

public interface IMapRepository
{
    public GameDefine.State[] Status { get; }
    
    public bool Load(int mapId);
}