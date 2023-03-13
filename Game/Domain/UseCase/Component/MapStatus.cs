using System;

namespace Souko.Game.Domain.UseCase.Component
{
    /// <summary>
    /// マップデータを扱いやすくするクラス.
    /// </summary>
    public class MapStatus : IDisposable
    {
        /// <summary>
        /// マップのサイズ.
        /// </summary>
        public int Length => _status.Length;
    
        /// <summary>
        /// マップの横サイズ.
        /// </summary>
        public int Width => _width;
        private readonly int _width;
    
        private GameDefine.State[] _status;

        public MapStatus(GameDefine.State[] status, int width)
        {
            _status = status;
            _width = width;
        }
    
        public MapStatus(MapStatus mapStatus)
        {
            _status = mapStatus._status.Clone() as GameDefine.State[];
            _width = mapStatus._width;
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

        public void Dispose()
        {
            _status = null;
        }
    }
}