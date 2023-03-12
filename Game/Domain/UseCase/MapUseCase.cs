using System;
using System.Collections.Generic;
using Souko.Game.Domain.Map;
using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Domain.UseCase;

/// <summary>
/// マップの制御を行う.
/// </summary>
public class MapUseCase
{
    public MapStatus Status => mapRepository.Status;
    
    private IMapRepository mapRepository;
    private IMapView mapView;

    private Vector2Int originalPlayerPos;
    private List<Vector2Int> originalGoalPos = new();

    public MapUseCase(IMapRepository mapRepository, IMapView mapView)
    {
        this.mapRepository = mapRepository;
        this.mapView = mapView;
    }

    public bool Load(int mapId, out OriginalPositionData originalPositionData)
    {
        if(!mapRepository.Load(mapId))
        {
            originalPositionData = null;
            return false;
        }
        
        // プレイヤー位置.
        originalPlayerPos = GetStatePosition(mapRepository.Status, GameDefine.State.Player);
        if (originalPlayerPos == GameDefine.InvalidPos)
        {
            Console.WriteLine("マップデータにプレイヤーが見つかりませんでした。");
            originalPositionData = null;
            return false;
        }
            
        // ゴール位置.
        originalGoalPos = GetStatePositions(mapRepository.Status, GameDefine.State.Goal);
        if (originalGoalPos.Count == 0)
        {
            Console.WriteLine("マップデータにゴールが見つかりませんでした。");
            originalPositionData = null;
            return false;
        }

        originalPositionData = new OriginalPositionData(originalPlayerPos, originalGoalPos);
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
    public List<Vector2Int> GetStatePositions(MapStatus status, GameDefine.State state)
    {
        var ret = new List<Vector2Int>();
        for (int i = 0; i < status.Length; i++)
        {
            if (status[i] == state)
            {
                ret.Add(i.ToVec2Int(GameDefine.MapLength));
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
    private Vector2Int GetStatePosition(MapStatus status, GameDefine.State state)
    {
        for (int i = 0; i < status.Length; i++)
        {
            if (status[i] == state)
            {
                return i.ToVec2Int(GameDefine.MapLength);
            }
        }

        Console.WriteLine($"指定した状態がマップ上に存在しません.\nstate:{state}");
        return GameDefine.InvalidPos;
    }

    /// <summary>
    /// 状態を更新.
    /// </summary>
    /// <param name="nowPosition"></param>
    /// <param name="nextPosition"></param>
    /// <param name="state"></param>
    public void UpdateStatus(Vector2Int nowPosition, Vector2Int nextPosition, GameDefine.State state)
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
    public bool CheckValidState(Vector2Int nextPosition, Vector2Int moveValue)
    {
        // マップ外.
        var isInvalidMapRange = !(0 <= nextPosition.x && nextPosition.x < GameDefine.MapLength &&
                                  0 <= nextPosition.y && nextPosition.y < GameDefine.MapLength); 
        if (isInvalidMapRange)
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