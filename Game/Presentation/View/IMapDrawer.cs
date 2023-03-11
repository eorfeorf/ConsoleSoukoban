using Souko.Game.Domain;

namespace Souko.Game.Presentation.View;

public interface IMapDrawer
{
    public void DrawIcon(GameDefine.State state);
    public void DrawNewLine();
}