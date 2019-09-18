using System;
using System.Collections.Generic;
using System.Text;

namespace EconoMe.Helpers
{
    public static class Constants
    {
#if RELEASE
        public static string BaseUrl = "<BaseUrl>";

        public static class Analytics
        {
            public static string AppSecretIOS = "ios=<ioskey>;";
            public static string AppSecretAndroid = "android=<androidkey>;";
        }
#else
        public static string BaseUrl = "<BaseUrl>";

        public static class Analytics
        {
            public static string AppSecretIOS = "ios=<ioskey>;";
            public static string AppSecretAndroid = "android=<androidkey>;";
        }
#endif
    }
}
