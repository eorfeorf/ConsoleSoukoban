namespace Souko.Game.Data.Map;

/// <summary>
/// マップデータを用意するクラス.
/// </summary>
public class MapDataStore : IMapDataStore
{
    public MapData Load(int mapId)
    {
        // TODO:外部データとして読み込み.
        // TODO:MapLoader作ったほうがいいかも・・・.
        var data = new[]
        {
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 3, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 2, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 2, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
            1, 0, 1, 0, 0, 1, 1, 1, 0, 1,
            1, 0, 1, 4, 0, 4, 0, 0, 0, 1,
            1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
        };
        var width = 10;
        return new(data, width);
    }
}