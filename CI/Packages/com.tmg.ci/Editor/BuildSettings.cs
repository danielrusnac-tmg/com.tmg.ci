using UnityEngine;

namespace Tmg.CI
{
    [CreateAssetMenu(menuName = "CI/CI Settings", fileName = "ci_")]
    public class BuildSettings : ScriptableObject
    {
        [SerializeField] private BuildMode[] _modes;

        public BuildMode GetBuildMode(int index)
        {
            if (index < 0 || index >= _modes.Length)
                return BuildMode.Default;

            return _modes[index];
        }
    }
}