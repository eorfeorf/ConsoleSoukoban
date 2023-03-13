using System;
using Souko.Game.Domain.Map;
using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Presentation.View
{
    /// <summary>
    /// CLIでマップ描画を行うクラス.
    /// </summary>
    public class MapViewCLI : IMapView
    {
        private MapViewCLIMapper _mapViewCLIMapper;
    
        public MapViewCLI(MapViewCLIMapper mapViewCLIMapper)
        {
            _mapViewCLIMapper = mapViewCLIMapper;
        }

        /// <summary>
        /// 描画.
        /// </summary>
        /// <param name="status"></param>
        public void Draw(MapStatus status)
        {
            // CLIでクリアするとちらつくので上書きする.
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
        
            for (int i = 0; i < status.Length; ++i)
            {
                if (i % status.Width == 0)
                {
                    Console.Write("\n");
                }

                var state = status[i];
                Console.Write(_mapViewCLIMapper.StateToIconTable[state]);
            }
        }
    }
}