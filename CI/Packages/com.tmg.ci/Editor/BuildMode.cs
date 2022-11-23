using System;
using UnityEditor;

namespace Tmg.CI
{
    [Serializable]
    public struct BuildMode
    {
        public string DefineSymbols;
        public BuildTargetGroup TargetGroup;
        public bool ShowSplashScreen;
        public bool IsDevelopment;
        public bool CheatsEnabled;
        public bool IncludeSdks;

        public static BuildMode Default = new()
        {
            DefineSymbols = "",
            TargetGroup = BuildTargetGroup.Android,
            ShowSplashScreen = false,
            IsDevelopment = false,
            CheatsEnabled = false,
            IncludeSdks = true
        };

        public static BuildMode Parse(string[] args)
        {
            return Default;
        }
    }
}