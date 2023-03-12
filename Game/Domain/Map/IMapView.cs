using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Domain.Map;

/// <summary>
/// マップの描画を吸収するインターフェイス.
/// </summary>
public interface IMapView
{
    public void Draw(MapStatus status);
}