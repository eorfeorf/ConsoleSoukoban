using Souko.Game.Domain.System;

namespace Souko.Game.Domain.UseCase;

public class LoggerUseCase
{
    private ILogger _logger;
    
    public LoggerUseCase(ILogger logger)
    {
        _logger = logger;
    }

    public void Log(string message)
    {
        _logger.Log(message);
    }
}