using System;
using Souko.Game.Domain.System;

namespace Souko.Game.Domain.UseCase
{
    /// <summary>
    /// ログ出力を制御するユースケース.
    /// </summary>
    public class LoggerUseCase : IDisposable
    {
        private readonly ILogger _logger;
    
        public LoggerUseCase(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 出力.
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            _logger.Log(message);
        }

        public void Dispose()
        {
        }
    }
}