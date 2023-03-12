using System;
using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Domain.UseCase;

/// <summary>
/// プレイヤーを制御するユースケース.
/// </summary>
public class PlayerUseCase : IDisposable
{
    /// <summary>
    /// 現在地のプレイヤーの位置.
    /// </summary>
    public Vector2Int Pos;

    private readonly LoggerUseCase _loggerUseCase;
    private readonly MapUseCase _mapUseCase;

    public PlayerUseCase(LoggerUseCase loggerUseCase, MapUseCase mapUseCase)
    {
        _loggerUseCase = loggerUseCase;
        _mapUseCase = mapUseCase;
    }

    /// <summary>
    /// プレイヤーの移動先座標を取得.
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public Vector2Int GetNextPosition(GameDefine.Dir dir)
    {
        return Pos + GameDefine.DirToMoveValue[dir];
    }
    
    /// <summary>
    /// 移動を適用.
    /// </summary>
    /// <param name="nowPosition"></param>
    /// <param name="nextPosition"></param>
    /// <param name="dir"></param>
    public void ApplyNextPosition(Vector2Int nowPosition, Vector2Int nextPosition, Vector2Int moveValue)
    {
        // 石の位置を更新.
        if (_mapUseCase.Status[nextPosition] == GameDefine.State.Stone)
        {
            var nextPosition2Ahead = nextPosition + moveValue;
            _mapUseCase.UpdateStatus(nextPosition, nextPosition2Ahead, GameDefine.State.Stone);
        }

        // プレイヤーの位置を更新.
        _mapUseCase.UpdateStatus(nowPosition, nextPosition, GameDefine.State.Player);
        Pos = nextPosition;

        // ゴールの位置を復活
        // Noneということはそのマスには誰もいない.
        foreach (var g in _mapUseCase.OriginalGoalPos)
        {
            if (_mapUseCase.Status[g] == GameDefine.State.None)
            {
                _mapUseCase.UpdateStatus(g, g, GameDefine.State.Goal);
            }
        }
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
        var isInvalidMapRange = !(0 <= nextPosition.x && nextPosition.x < _mapUseCase.Status.Width &&
                                  0 <= nextPosition.y && nextPosition.y < _mapUseCase.Status.Width); 
        if (isInvalidMapRange)
        {
            return false;
        }

        // 移動先が有効な状態か.
        var state = _mapUseCase.Status[nextPosition];
            
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
            var state2 = _mapUseCase.Status[nextPosition2Ahead];
            if (state2 == GameDefine.State.Wall || state2 == GameDefine.State.Stone)
            {
                return false;
            }
        }
            
        return true;
    }

    public void Dispose()
    {
    }
}