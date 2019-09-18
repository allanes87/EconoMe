using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using EconoMe.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EconoMe.Services.LogService
{
    public class LoggerService : ILoggerService
    {
        public void InitializeAnalytics()
        {
            AppCenter.Start(Constants.Analytics.AppSecretIOS + ";" +
                Constants.Analytics.AppSecretAndroid,
                typeof(Microsoft.AppCenter.Analytics.Analytics), typeof(Crashes));
        }

        public void RegisterNavigationEvent(Type viewModelType, object parameter)
        {
            string parameterData = "null";

            if (parameter != null)
                parameterData = parameter.ToString();

            Microsoft.AppCenter.Analytics.Analytics.TrackEvent("Navigation", new Dictionary<string, string> {
                { "View", viewModelType.Name },
                { "Parameter", parameterData }
            });
        }

        public void RegisterCustomEvent(string eventName, Dictionary<string, string> properties)
        {
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent(eventName, properties);
        }

        public void LogError(Exception ex)
        {
            Crashes.TrackError(ex);
        }

        public void LogError(Exception ex, Dictionary<string, string> properties)
        {
            Crashes.TrackError(ex, properties);
        }
    }
}
