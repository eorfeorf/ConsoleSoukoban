using System;
using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Domain.UseCase;

public class InGameFrameworkUseCase
{
    private LoggerUseCase _loggerUseCase;
    private MapUseCase mapUseCase;
    private InputUseCase inputUseCase;
    private PlayerUseCase playerUseCase;
    private GameFlowUseCase gameFlowUseCase;
    
    public InGameFrameworkUseCase(LoggerUseCase loggerUseCase, MapUseCase mapUseCase, InputUseCase inputUseCase, PlayerUseCase playerUseCase, GameFlowUseCase gameFlowUseCase)
    {
        _loggerUseCase = loggerUseCase;
        this.mapUseCase = mapUseCase;
        this.inputUseCase = inputUseCase;
        this.playerUseCase = playerUseCase;
        this.gameFlowUseCase = gameFlowUseCase;
    }

    /// <summary>
    /// 初期化。マップ読み込み等.
    /// </summary>
    /// <param name="mapId"></param>
    /// <returns>false:正常 true:異常</returns>
    public bool Initialize(int mapId)
    {
        // マップ読み込み.
        if (!mapUseCase.Load(mapId))
        {
            _loggerUseCase.Log("マップデータが不正でした。\n");
            return false;
        }

        playerUseCase.Pos = mapUseCase.OriginalPlayerPos;
        return true;
    }
    
    /// <summary>
    /// ゲームループ更新.
    /// </summary>
    /// <returns></returns>
    public void Update()
    {
        // 入力更新.
        inputUseCase.UpdateInput();

        // リセット.
        if (inputUseCase.GetReset())
        {
            Initialize(0);
        }
                
        // プレイヤー移動.
        var dir = inputUseCase.GetDir();
        if (dir != GameDefine.Dir.None)
        {
            // 不正移動先判定.
            var nextPosition = playerUseCase.GetNextPosition(dir);
            bool isValidState = mapUseCase.CheckValidState(nextPosition, GameDefine.DirToMoveIndex[(int)dir]);
            if (!isValidState)
            {
                return;
            }

            // 移動適用.
            playerUseCase.ApplyNextPosition(playerUseCase.Pos, nextPosition, GameDefine.DirToMoveIndex[(int)dir]);
        }
    }

    /// <summary>
    /// ゲーム終了したか.
    /// </summary>
    /// <returns></returns>
    public bool IsEnd()
    {
        return gameFlowUseCase.IsGameEnd(mapUseCase.OriginalGoalPos);
    }

    /// <summary>
    /// 描画.
    /// </summary>
    public void Draw()
    {
        mapUseCase.Draw();
    }
}