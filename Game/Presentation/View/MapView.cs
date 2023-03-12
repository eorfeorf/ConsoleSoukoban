using Souko.Game.Domain;
using Souko.Game.Domain.Map;
using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Presentation.View;

public class MapView : IMapView
{
    private readonly IMapDrawer mapDrawer;
    private readonly IMapViewSetupper mapViewSetupper;

    public MapView(IMapDrawer mapDrawer, IMapViewSetupper mapViewSetupper)
    {
        this.mapDrawer = mapDrawer;
        this.mapViewSetupper = mapViewSetupper;
    }

    //TODO:ここクソコード.
    public void Draw(MapStatus status)
    {
        mapViewSetupper.Setup();
        
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