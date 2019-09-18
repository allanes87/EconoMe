using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace EconoMe.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants


        private const string IdUseMocks = "use_mocks";
        private static readonly bool UseMocksDefault = false;

        private const string IdUrlBase = "url_base";
        private static readonly string UrlBaseDefault = string.Empty;

        #endregion

        public static bool UseMocks
        {
            get
            {
                return AppSettings.GetValueOrDefault(IdUseMocks, UseMocksDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(IdUseMocks, value);
            }
        }

        public static string UrlBase
        {
            get
            {
                return AppSettings.GetValueOrDefault(IdUrlBase, UrlBaseDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(IdUrlBase, value);
            }
        }
    }
}
