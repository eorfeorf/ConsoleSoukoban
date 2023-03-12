namespace Souko.Game.Domain.UseCase.Component;

/// <summary>
/// マップデータを扱いやすくするクラス.
/// </summary>
public class MapStatus
{
    /// <summary>
    /// マップの横サイズ.
    /// </summary>
    public int Width => _status.Length;
    
    private readonly GameDefine.State[] _status;
    private readonly int _width;

    public MapStatus(GameDefine.State[] status, int width)
    {
        _status = status;
        _width = width;
    }
    
    /// <summary>
    /// 添え字対応.
    /// </summary>
    /// <param name="i"></param>
    public GameDefine.State this[int i]
    {
        get => _status[i];
        set => _status[i] = value;
    }
    
    /// <summary>
    /// 座標対応.
    /// </summary>
    /// <param name="v"></param>
    public GameDefine.State this[Vector2Int v]
    {
        get => _status[v.x + v.y * _width];
        set => _status[v.x + v.y * _width] = value;
    }
}