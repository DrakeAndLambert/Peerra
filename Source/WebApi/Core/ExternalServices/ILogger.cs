using System;

namespace DrakeLambert.Peerra.WebApi.Core.ExternalServices
{
    public interface ILogger<T>
    {
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void LogError(string message, Exception exception, params object[] args);
    }
}
