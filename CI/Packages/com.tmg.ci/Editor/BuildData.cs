using System;
using UnityEditor;

namespace TMG.Builder
{
    public struct BuildData
    {
        public bool ExitAfter;
        public string BuildPath;
        public BuildTarget BuildTarget;
        public bool LogsEnabled;
        public bool ContainsSDKs;
        public string BundleVersion;
        public string BuildNumber;

        public static BuildData Parse(string[] args)
        {
            BuildData data = new BuildData();

            data.BuildPath = args[7];
            Enum.TryParse(args[8], true, out data.BuildTarget);
            bool.TryParse(args[9], out data.LogsEnabled);
            bool.TryParse(args[10], out data.ContainsSDKs);
            data.ExitAfter = true;

            if (data.ContainsSDKs)
            {
                data.BundleVersion = args[11];
                data.BuildNumber = args[12];
            }

            return data;
        }

        public static BuildData CreateLocal()
        {
            BuildPlayerOptions playerSettings =
                BuildPlayerWindow.DefaultBuildMethods.GetBuildPlayerOptions(new BuildPlayerOptions());

            BuildData data = new BuildData()
            {
                BuildPath = playerSettings.locationPathName,
                BuildNumber = "",
                BundleVersion = "local",
                BuildTarget = playerSettings.target,
                ExitAfter = false,
                LogsEnabled = true,
                ContainsSDKs = false
            };

            return data;
        }
    }
}