namespace Souko.Game.Data.Map;

/// <summary>
/// マップデータ.
/// </summary>
public class MapData
{
    public int Id { get; }
    // TODO:二次元配列にするかも.そしたらWidthいらない.
    public int[] Data { get; }
    public int Width { get; }

    public MapData(int id, int[] data, int width)
    {
        Id = id;
        Data = data;
        Width = width;
    }
}