using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Domain.Map;

public interface IMapView
{
    public void Draw(MapStatus status);
}