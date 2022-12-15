using System;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace TMG.CI
{
    // D:\Unity\2020.3.0f1\Editor\Unity.exe -quit -batchMode -projectPath . -executeMethod GameBuilder.ChangeSettings -defines=FOO;BAR -buildTarget Android
    // D:\Unity\2020.3.0f1\Editor\Unity.exe -quit -batchMode -projectPath . -executeMethod GameBuilder.BuildContentAndPlayer -buildTarget Android
    
    public static class GameBuilder
    {
        private const string BUILD_SCRIPT = "Assets/AddressableAssetsData/DataBuilders/BuildScriptPackedMode.asset";
        private const string PROFILE_NAME = "Default";

        public static void ChangeSettings()
        {
            string defines = "";
            string[] args = Environment.GetCommandLineArgs();

            foreach (string arg in args)
                if (arg.StartsWith("-defines=", StringComparison.CurrentCulture))
                    defines = arg.Substring(("-defines=".Length));

            BuildTargetGroup buildSettings = EditorUserBuildSettings.selectedBuildTargetGroup;
            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildSettings, defines);
        }

        public static void BuildContentAndPlayer()
        {
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            settings.activeProfileId = settings.profileSettings.GetProfileId(PROFILE_NAME);
            IDataBuilder builder = AssetDatabase.LoadAssetAtPath<ScriptableObject>(BUILD_SCRIPT) as IDataBuilder;
            settings.ActivePlayerDataBuilderIndex = settings.DataBuilders.IndexOf((ScriptableObject)builder);

            AddressableAssetSettings.BuildPlayerContent(out AddressablesPlayerBuildResult result);

            if (!string.IsNullOrEmpty(result.Error))
                throw new Exception(result.Error);

            BuildReport buildReport = BuildPipeline.BuildPlayer(
                EditorBuildSettings.scenes, 
                "d:/build/winApp.exe",
                EditorUserBuildSettings.activeBuildTarget, 
                BuildOptions.None);

            if (buildReport.summary.result != BuildResult.Succeeded)
                throw new Exception(buildReport.summary.ToString());
        }

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
            AddressableAssetSettings.CleanPlayerContent();
            AddressableAssetSettings.BuildPlayerContent();
        }

        private static void Exit(int status, BuildData data)
        {
            if (!data.ExitAfter)
                return;

            EditorApplication.Exit(status);
        }
    }
}