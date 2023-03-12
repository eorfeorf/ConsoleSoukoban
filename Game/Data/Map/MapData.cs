namespace Souko.Game.Data.Map;

/// <summary>
/// マップデータ.
/// </summary>
public class MapData
{
    // TODO:二次元配列にするかも.そしたらWidthいらない.
    public int[] Data { get; }
    public int Width { get; }

    public MapData(int[] data, int width)
    {
        Data = data;
        Width = width;
    }
}