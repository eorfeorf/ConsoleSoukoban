using System;
using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Domain.UseCase;

public class InGameFrameworkUseCase
{
    private readonly LoggerUseCase _loggerUseCase;
    private readonly MapUseCase _mapUseCase;
    private readonly InputUseCase _inputUseCase;
    private readonly PlayerUseCase _playerUseCase;
    private readonly GameFlowUseCase _gameFlowUseCase;
    
    public InGameFrameworkUseCase(LoggerUseCase loggerUseCase, MapUseCase mapUseCase, InputUseCase inputUseCase, PlayerUseCase playerUseCase, GameFlowUseCase gameFlowUseCase)
    {
        _loggerUseCase = loggerUseCase;
        _mapUseCase = mapUseCase;
        _inputUseCase = inputUseCase;
        _playerUseCase = playerUseCase;
        _gameFlowUseCase = gameFlowUseCase;
    }

    /// <summary>
    /// 初期化。マップ読み込み等.
    /// </summary>
    /// <param name="mapId"></param>
    /// <returns>false:正常 true:異常</returns>
    public bool Initialize(int mapId)
    {
        // マップ読み込み.
        if (!_mapUseCase.Load(mapId))
        {
            _loggerUseCase.Log("マップデータが不正でした。\n");
            return false;
        }

        _playerUseCase.Pos = _mapUseCase.OriginalPlayerPos;
        return true;
    }
    
    /// <summary>
    /// ゲームループ更新.
    /// </summary>
    /// <returns>ゲームが終了したか</returns>
    public bool Update()
    {
        if (_gameFlowUseCase.IsGameEnd(_mapUseCase.OriginalGoalPos))
        {
            return true;
        }
        
        // 入力更新.
        _inputUseCase.UpdateInput();

        // リセット.
        if (_inputUseCase.GetReset())
        {
            Initialize(0);
        }
                
        // プレイヤー移動.
        var dir = _inputUseCase.GetDir();
        if (dir != GameDefine.Dir.None)
        {
            // 不正移動先判定.
            var nextPosition = _playerUseCase.GetNextPosition(dir);
            bool isValidState = _mapUseCase.CheckValidState(nextPosition, GameDefine.DirToMoveIndex[(int)dir]);
            if (!isValidState)
            {
                return false;
            }

            // 移動適用.
            _playerUseCase.ApplyNextPosition(_playerUseCase.Pos, nextPosition, GameDefine.DirToMoveIndex[(int)dir]);
        }

        return false;
    }

    /// <summary>
    /// 描画.
    /// </summary>
    public void Draw()
    {
        _mapUseCase.Draw();
    }
}