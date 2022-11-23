using UnityEngine;

namespace Tmg.CI
{
    [CreateAssetMenu(menuName = "CI/Build Settings", fileName = "build_")]
    public class BuildSettings : ScriptableObject
    {
        [SerializeField] private BuildMode _mode = BuildMode.Default;

        [ContextMenu(nameof(Build))]
        public void Build()
        {
            GameBuilder.Build(_mode);
        }
    }
}