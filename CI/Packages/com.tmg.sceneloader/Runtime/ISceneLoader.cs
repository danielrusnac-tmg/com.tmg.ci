using System.Collections;
using UnityEditor;

namespace TMG.SceneLoader
{
    public interface ISceneLoader
    {
        bool IsLoaded(SceneAsset scene);
        IEnumerator Load(SceneAsset scene);
        IEnumerator Unload(SceneAsset scene);
    }
}