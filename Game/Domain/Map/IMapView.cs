namespace Souko.Game.Domain.Map;

public interface IMapView
{
    public void Draw(GameDefine.State[] status);
}