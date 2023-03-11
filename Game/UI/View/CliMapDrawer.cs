using Souko.Game.Domain;

namespace Souko.Game.Presentation.View;

public class CliMapDrawer : IMapDrawer
{
    private IDrawer drawer;
    
    // タイプによるアイコンテーブル.
    private static readonly string[] StateToIconTable =
    {
        "　",
        "壁",
        "石",
        "〇",
        "ロ",
    };

    public CliMapDrawer(IDrawer drawer)
    {
        this.drawer = drawer;
    }
    
    public void DrawIcon(GameDefine.State state)
    {
        drawer.Draw(StateToIconTable[(int)state]);
    }

    public void DrawNewLine()
    {
        drawer.Draw("\n");
    }
}