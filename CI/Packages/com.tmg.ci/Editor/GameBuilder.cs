using System;
using UnityEditor;

namespace Tmg.CI
{
    public static class GameBuilder
    {
        public static void BuildWithArgumentsAndExit()
        {
            Build(BuildMode.Parse(Environment.GetCommandLineArgs()));
            EditorApplication.Exit(0);
        }

        public static void Build(BuildMode mode)
        {
            ApplyBuildSettings(mode);
            
            // build
        }

        private static void ApplyBuildSettings(BuildMode mode)
        {
            string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
        }
    }
}