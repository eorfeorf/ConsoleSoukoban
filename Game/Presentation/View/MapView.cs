using Souko.Game.Domain;
using Souko.Game.Domain.Map;

namespace Souko.Game.Presentation.View;

public class MapView : IMapView
{
    private readonly IMapDrawer mapDrawer;

    public MapView(IMapDrawer mapDrawer)
    {
        this.mapDrawer = mapDrawer;
    }

    // TODO:ここの処理がCLIに引きづられている.
    // IDrawer -> IMapDrawer
    // IMapDrawerで吸収
    // IDrawer
    public void Draw(GameDefine.State[] status)
    {
        for (int i = 0; i < status.Length; ++i)
        {
            if (i % GameDefine.MapLength == 0)
            {
                mapDrawer.DrawNewLine();
            }
         
            mapDrawer.DrawIcon(status[i]);
        }
    }
}