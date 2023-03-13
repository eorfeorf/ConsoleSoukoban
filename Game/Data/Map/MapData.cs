namespace Souko.Game.Data.Map
{
    /// <summary>
    /// マップデータ.
    /// </summary>
    public class MapData
    {
        public int Id { get; }
        public int[,] Status { get; }
        public int Width { get; }

        public MapData(int id, int[,] status, int width)
        {
            Id = id;
            Status = status;
            Width = width;
        }
    }
}