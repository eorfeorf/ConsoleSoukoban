﻿using System.Collections.Generic;
using Souko.Game.Domain.Map;
using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Domain.UseCase;

/// <summary>
/// マップの制御するユースケース.
/// </summary>
public class MapUseCase
{
    /// <summary>
    /// マップの状態.
    /// </summary>
    public MapStatus Status => _mapRepository.Status;
    /// <summary>
    /// プレイヤーの初期値.
    /// </summary>
    public Vector2Int OriginalPlayerPos => _originalPlayerPos;
    /// <summary>
    /// ゴールの初期値.
    /// </summary>
    public IList<Vector2Int> OriginalGoalPos => _originalOriginalGoalPoPos;

    private readonly LoggerUseCase _loggerUseCase;
    private readonly IMapRepository _mapRepository;
    private readonly IMapView _mapView;
    
    private Vector2Int _originalPlayerPos;
    private List<Vector2Int> _originalOriginalGoalPoPos = new();

    public MapUseCase(LoggerUseCase loggerUseCase, IMapRepository mapRepository, IMapView mapView)
    {
        _loggerUseCase = loggerUseCase;
        _mapRepository = mapRepository;
        _mapView = mapView;
    }

    /// <summary>
    /// マップ読み込み.
    /// </summary>
    /// <param name="mapId"></param>
    /// <returns></returns>
    public bool Load(int mapId)
    {
        if(!_mapRepository.Load(mapId))
        {
            return false;
        }
        
        // プレイヤー位置.
        _originalPlayerPos = GetStatePosition(_mapRepository.Status, GameDefine.State.Player);
        if (_originalPlayerPos == GameDefine.InvalidPos)
        {
            _loggerUseCase.Log("マップデータにプレイヤーが見つかりませんでした。\n");
            return false;
        }
            
        // ゴール位置.
        _originalOriginalGoalPoPos = GetStatePositions(_mapRepository.Status, GameDefine.State.Goal);
        if (_originalOriginalGoalPoPos.Count == 0)
        {
            _loggerUseCase.Log("マップデータにゴールが見つかりませんでした。\n");
            return false;
        }

        return true;
    }

    /// <summary>
    /// マップ描画.
    /// </summary>
    public void Draw()
    {
        _mapView.Draw(_mapRepository.Status);
    }

    /// <summary>
    /// マップ状態を更新.
    /// </summary>
    /// <param name="nowPosition"></param>
    /// <param name="nextPosition"></param>
    /// <param name="state"></param>
    public void UpdateStatus(Vector2Int nowPosition, Vector2Int nextPosition, GameDefine.State state)
    {
        _mapRepository.Status[nowPosition] = GameDefine.State.None;
        _mapRepository.Status[nextPosition] = state;
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
        var state = _mapRepository.Status[nextPosition];
            
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
            var state2 = _mapRepository.Status[nextPosition2Ahead];
            if (state2 == GameDefine.State.Wall || state2 == GameDefine.State.Stone)
            {
                return false;
            }
        }
            
        return true;
    }

    /// <summary>
    /// ある状態の座標を取得.
    /// </summary>
    /// <param name="status"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    private List<Vector2Int> GetStatePositions(MapStatus status, GameDefine.State state)
    {
        var ret = new List<Vector2Int>();
        for (int i = 0; i < status.Width; i++)
        {
            if (status[i] == state)
            {
                ret.Add(i.ToVec2Int(GameDefine.MapLength));
            }
        }

        if(ret.Count == 0)
        {
            _loggerUseCase.Log($"指定した状態がマップ上に存在しません. state:{state}\n");
        }
            
        return ret;
    }

    /// <summary>
    /// ある状態の座標を取得.
    /// </summary>
    /// <param name="status"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    private Vector2Int GetStatePosition(MapStatus status, GameDefine.State state)
    {
        for (int i = 0; i < status.Width; i++)
        {
            if (status[i] == state)
            {
                return i.ToVec2Int(GameDefine.MapLength);
            }
        }

        _loggerUseCase.Log($"指定した状態がマップ上に存在しません. state:{state}\n");
        return GameDefine.InvalidPos;
    }
}