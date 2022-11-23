using UnityEditor.Experimental;

namespace Tmg.CI
{
    internal static class SettingsProvider
    {
        public static BuildSettings LoadSettings(string name)
        {
            return EditorResources.Load<BuildSettings>($"Builds/{name}");
        }
    }
}