using System;
using System.Collections.Generic;
using Souko.Game.Domain.Map;

namespace Souko.Game.Domain.UseCase;

/// <summary>
/// マップの制御を行う.
/// </summary>
public class MapUseCase
{
    public GameDefine.State[] Status => mapRepository.Status;
    
    private IMapRepository mapRepository;
    private IMapView mapView;

    public MapUseCase(IMapRepository mapRepository, IMapView mapView)
    {
        this.mapRepository = mapRepository;
        this.mapView = mapView;
    }

    public bool Load(int mapId)
    {
        return mapRepository.Load(mapId);
    }

    public void Draw()
    {
        mapView.Draw(mapRepository.Status);
    }
        
    /// <summary>
    /// ある状態の座標を取得.
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public List<int> GetStatePositions(GameDefine.State state)
    {
        var ret = new List<int>();
        for (int i = 0; i < mapRepository.Status.Length; i++)
        {
            if (mapRepository.Status[i] == state)
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
        for (int i = 0; i < mapRepository.Status.Length; i++)
        {
            if (mapRepository.Status[i] == state)
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
        mapRepository.Status[nowPosition] = GameDefine.State.None;
        mapRepository.Status[nextPosition] = state;
    }
}