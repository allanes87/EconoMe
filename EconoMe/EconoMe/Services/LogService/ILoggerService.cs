using System;
using System.Collections.Generic;
using System.Text;

namespace EconoMe.Services.LogService
{
    public interface ILoggerService
    {
        void InitializeAnalytics();
        void RegisterNavigationEvent(Type viewModelType, object parameter);
        void RegisterCustomEvent(string eventName, Dictionary<string, string> properties);

        void LogError(Exception ex);
        void LogError(Exception ex, Dictionary<string, string> properties);
    }
}
