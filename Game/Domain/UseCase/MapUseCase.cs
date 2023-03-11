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
    public int OriginalPlayerPos => originalPlayerPos;
    public List<int> OriginalGoalPos => originalGoalPos;
    
    private IMapRepository mapRepository;
    private IMapView mapView;

    private int originalPlayerPos;
    private List<int> originalGoalPos = new();

    public MapUseCase(IMapRepository mapRepository, IMapView mapView)
    {
        this.mapRepository = mapRepository;
        this.mapView = mapView;
    }

    public bool Load(int mapId)
    {
        if(!mapRepository.Load(mapId))
        {
            return false;
        }
        
        // プレイヤー位置.
        originalPlayerPos = GetStatePosition(mapRepository.Status, GameDefine.State.Player);
        if (originalPlayerPos == GameDefine.InvalidIndex)
        {
            Console.WriteLine("マップデータにプレイヤーが見つかりませんでした。");
            return false;
        }
            
        // ゴール位置.
        originalGoalPos = GetStatePositions(mapRepository.Status, GameDefine.State.Goal);
        if (originalGoalPos.Count == 0)
        {
            Console.WriteLine("マップデータにゴールが見つかりませんでした。");
            return false;
        }
        
        return true;
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
    public List<int> GetStatePositions(GameDefine.State[] status, GameDefine.State state)
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
    private int GetStatePosition(GameDefine.State[] status, GameDefine.State state)
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
        mapRepository.Status[nowPosition] = GameDefine.State.None;
        mapRepository.Status[nextPosition] = state;
    }
    
    /// <summary>
    /// 移動先が正常な状態か.
    /// </summary>
    /// <param name="nextPosition"></param>
    /// <param name="moveValue"></param>
    /// <returns></returns>
    public bool CheckValidState(int nextPosition, int moveValue)
    {
        // マップ外.
        var isValidMapRange = 0 <= nextPosition && nextPosition < mapRepository.Status.Length;
        if (!isValidMapRange)
        {
            return false;
        }

        // 移動先が有効な状態か.
        var state = mapRepository.Status[nextPosition];
            
        // 壁.
        if (state == GameDefine.State.Wall)
        {
            return false;
        }
            
        // 石.
        if (state == GameDefine.State.Stone)
        {
            // 石の先が移動できるか.
            var nextPosition2Ahead = nextPosition + moveValue;
            var state2 = mapRepository.Status[nextPosition2Ahead];
            if (state2 == GameDefine.State.Wall || state2 == GameDefine.State.Stone)
            {
                return false;
            }
        }
            
        return true;
    }
}