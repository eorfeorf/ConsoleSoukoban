namespace Souko.Game.Domain.UseCase.Component
{
    /// <summary>
    /// インデックスを座標に変換するためのint型拡張.
    /// </summary>
    public static class IntExtension
    {
        /// <summary>
        /// 変換.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static Vector2Int ToVec2Int(this int x, int width)
        {
            return new Vector2Int(x % width, x / width);
        }
    }
}