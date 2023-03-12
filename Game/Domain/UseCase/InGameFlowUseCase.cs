using System.Collections.Generic;
using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Domain.UseCase;

public class InGameFlowUseCase
{
    private MapUseCase _mapUseCase;
    
    public InGameFlowUseCase(MapUseCase mapUseCase)
    {
        _mapUseCase = mapUseCase;
    }
    
    /// <summary>
    /// 終了判定.
    /// </summary>
    /// <returns></returns>
    public bool IsGameEnd(IList<Vector2Int> goals)
    {
        foreach (var g in goals)
        {
            var nowState = _mapUseCase.Status[g];
            if (nowState != GameDefine.State.Stone)
            {
                return false;
            }
        }

        return true;
    }
    
}