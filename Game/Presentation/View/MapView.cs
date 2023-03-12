using System;
using Souko.Game.Domain;
using Souko.Game.Domain.Map;
using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Presentation.View;

public class MapView : IMapView
{
    private readonly IDrawer _drawer;
    
    // タイプによるアイコンテーブル.
    private static readonly string[] StateToIconTable =
    {
        "　",
        "壁",
        "石",
        "〇",
        "ロ",
    };

    public MapView(IDrawer drawer)
    {
        _drawer = drawer;
    }

    // TODO:コンソールに依存しない方法を探したい.
    public void DrawSetup()
    {
        Console.CursorLeft = 0;
        Console.CursorTop = 0;
    }

    public void Draw(MapStatus status)
    {
        for (int i = 0; i < status.Length; ++i)
        {
            if (i % GameDefine.MapLength == 0)
            {
                _drawer.Draw("\n");
            }

            var state = status[i];
            _drawer.Draw(StateToIconTable[(int)state]);
        }
    }
}