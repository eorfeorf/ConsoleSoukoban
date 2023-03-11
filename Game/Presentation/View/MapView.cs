﻿using System;
using Souko.Game.Domain;
using Souko.Game.Domain.Map;

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

    public void Draw(GameDefine.State[] status)
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