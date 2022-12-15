using System;
using UnityEditor;

namespace TMG.Builder
{
    public static class GameBuilder
    {
        public static void BuildInBatchMode()
        {
            Build(BuildData.Parse(Environment.GetCommandLineArgs()));
        }

        public static void Build(BuildData data)
        {
            ApplyBuildSettings(data);
            BuildAddressables();
            BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, data.BuildPath, data.BuildTarget, BuildOptions.None);
            Exit(0, data);
        }

        private static void ApplyBuildSettings(BuildData data)
        {
            PlayerSettings.SplashScreen.show = false;

            if (data.ContainsSDKs)
            {
                if (data.BuildTarget == BuildTarget.Android)
                {
                    PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
                    PlayerSettings.Android.targetArchitectures = AndroidArchitecture.All;
                }

                PlayerSettings.bundleVersion = data.BundleVersion;
                PlayerSettings.iOS.buildNumber = data.BuildNumber;
            }

            EditorUserBuildSettings.exportAsGoogleAndroidProject =
                data.ContainsSDKs && data.BuildTarget == BuildTarget.Android;
        }

        private static void BuildAddressables()
        {
            
        }

        private static void Exit(int status, BuildData data)
        {
            if (!data.ExitAfter)
                return;

            EditorApplication.Exit(status);
        }
    }
}