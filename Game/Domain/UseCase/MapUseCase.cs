using System;
using System.Collections.Generic;
using System.Linq;
using Souko.Game.Domain.Map;

namespace Souko.Game.Domain
{
    /// <summary>
    /// マップの制御を行う.
    /// </summary>
    public class MapUseCase
    {
        public GameDefine.State[] Status => status;
        
        private IMapRepository mapRepository;

        private static GameDefine.State[] status;

        public MapUseCase(IMapRepository mapRepository)
        {
            this.mapRepository = mapRepository;
        }

        public bool Load(int mapId)
        {
            status = mapRepository.Load(mapId).Select(x => (GameDefine.State) x).ToArray();
            return status != null;
        }
        
        /// <summary>
        /// ある状態の座標を取得.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public List<int> GetStatePositions(GameDefine.State state)
        {
            var ret = new List<int>();
            for (int i = 0; i < status.Length; i++)
            {
                if (status[i] == state)
                {
                    ret.Add(i);
                }
            }

            if(ret.Count == 0)
            {
                Console.WriteLine($"指定した状態がマップ上に存在しません.\nstate:{state}");
            }
            
            return ret;
        }
        
        /// <summary>
        /// ある状態の座標を取得.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public int GetStatePosition(GameDefine.State state)
        {
            for (int i = 0; i < status.Length; i++)
            {
                if (status[i] == state)
                {
                    return i;
                }
            }

            Console.WriteLine($"指定した状態がマップ上に存在しません.\nstate:{state}");
            return GameDefine.InvalidIndex;
        }

        /// <summary>
        /// 状態を更新.
        /// </summary>
        /// <param name="nowPosition"></param>
        /// <param name="nextPosition"></param>
        /// <param name="state"></param>
        public void UpdateStatus(int nowPosition, int nextPosition, GameDefine.State state)
        {
            status[nowPosition] = GameDefine.State.None;
            status[nextPosition] = state;
        }
    }
}