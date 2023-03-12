using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Domain.Map;

public interface IMapRepository
{
    public MapStatus Status { get; }
    
    public bool Load(int mapId);
}