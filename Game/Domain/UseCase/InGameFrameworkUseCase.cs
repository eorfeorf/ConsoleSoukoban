using System.Collections.Generic;
using Souko.Game.Domain.UseCase.Component;

namespace Souko.Game.Domain.UseCase
{
    /// <summary>
    /// インゲーム全体を制御するユースケース.
    /// </summary>
    public class InGameFrameworkUseCase
    {
        private readonly LoggerUseCase _loggerUseCase;
        private readonly MapUseCase _mapUseCase;
        private readonly InputUseCase _inputUseCase;
        private readonly PlayerUseCase _playerUseCase;
    
        public InGameFrameworkUseCase(LoggerUseCase loggerUseCase, MapUseCase mapUseCase, InputUseCase inputUseCase, PlayerUseCase playerUseCase)
        {
            _loggerUseCase = loggerUseCase;
            _mapUseCase = mapUseCase;
            _inputUseCase = inputUseCase;
            _playerUseCase = playerUseCase;
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

        ~InGameFrameworkUseCase()
        {
            _loggerUseCase.Dispose();
            _mapUseCase.Dispose();
            _inputUseCase.Dispose();
            _playerUseCase.Dispose();
        }

        /// <summary>
        /// ゲームループ更新.
        /// </summary>
        /// <returns>ゲームが終了したか</returns>
        public bool Update()
        {
            // 終了したか？
            if (IsGameEnd(_mapUseCase.OriginalGoalPos))
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
                var isValidState = _playerUseCase.CheckValidState(nextPosition, GameDefine.DirToMoveValue[dir]);
                if (!isValidState)
                {
                    return false;
                }

                // 移動適用.
                _playerUseCase.ApplyNextPosition(_playerUseCase.Pos, nextPosition, GameDefine.DirToMoveValue[dir]);
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
    
        /// <summary>
        /// 終了判定.
        /// </summary>
        /// <returns></returns>
        private bool IsGameEnd(IList<Vector2Int> goals)
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
}