using UnityEditor;

namespace TMG.CI
{
    internal static class MenuItems
    {
        [MenuItem("GameRig/Build")]
        public static void PerformBuildLocal()
        {
            GameBuilder.Build(BuildData.CreateLocal());
        }
    }
}