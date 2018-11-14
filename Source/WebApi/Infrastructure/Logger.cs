using System;
using Microsoft.Extensions.Logging;

namespace DrakeLambert.Peerra.WebApi.Infrastructure
{
    public class Logger<T> : Core.ExternalServices.ILogger<T>
    {
        private readonly Microsoft.Extensions.Logging.ILogger<T> _logger;

        public Logger(Microsoft.Extensions.Logging.ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogError(string message, Exception exception, params object[] args)
        {
            _logger.LogError(message, exception, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }
    }
}
