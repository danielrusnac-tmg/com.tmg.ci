using System;

namespace Tmg.CI
{
    [Serializable]
    public struct BuildMode
    {
        public string Name;
        public string Preprocessor;
        public bool ShowSplashScreen;
        public bool IsDevelopment;
        public bool CheatsEnabled;
        public bool IncludeSdks;

        public static BuildMode Default = new()
        {
            Name = "Default", 
            Preprocessor = "",
            ShowSplashScreen = false,
            IsDevelopment = false,
            CheatsEnabled = false,
            IncludeSdks = true
        };
    }
}