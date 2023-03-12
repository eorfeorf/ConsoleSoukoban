namespace Souko.Game.Domain.UseCase.Component;

public class MapStatus
{
    public int Length => _status.Length;
    
    private GameDefine.State[] _status;

    private int _length;
    
    public GameDefine.State this[int i]
    {
        get { return _status[i]; }
        set { _status[i] = value; }
    }
    public GameDefine.State this[Vector2Int v]
    {
        get { return _status[v.x + v.y * _length]; }
        set { _status[v.x + v.y * _length] = value; }
    }

    public MapStatus(GameDefine.State[] status, int length)
    {
        _status = status;
        _length = length;
    }
}