using System;
using UnityEditor;

namespace Tmg.CI
{
    public static class GameBuilder
    {
        public static void BuildInBatchMode()
        {
            Build(GetBuildDataFromArguments());
            EditorApplication.Exit(0);
        }

        private static void Build(BuildData data)
        {
            ApplyBuildSettings(SettingsProvider.LoadSettings().GetBuildMode(data.BuildMode));
        }

        private static void ApplyBuildSettings(BuildMode getBuildMode)
        {
            throw new NotImplementedException();
        }

        private static BuildData GetBuildDataFromArguments()
        {
            string[] args = Environment.GetCommandLineArgs();
            return new BuildData();
        }

        private struct BuildData
        {
            public int BuildMode;
        }
    }
}