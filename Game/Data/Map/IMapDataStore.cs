namespace Souko.Game.Data.Map;

/// <summary>
/// マップデータを用意するインターフェイス.
/// </summary>
public interface IMapDataStore
{
    /// <summary>
    /// 外部からマップを読み込み.
    /// </summary>
    /// <param name="mapId"></param>
    /// <returns></returns>
    public MapData Load(int mapId);
}