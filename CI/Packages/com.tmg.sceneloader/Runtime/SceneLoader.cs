using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TMG.SceneLoader
{
    public class SceneLoader : MonoBehaviour, ISceneLoader
    {
        private HashSet<SceneAsset> _loadedScenes = new();

        public bool IsLoaded(SceneAsset scene) => _loadedScenes.Contains(scene);

        public IEnumerator Load(SceneAsset scene)
        {
            if (!_loadedScenes.Add(scene))
                yield break;
            
            yield return SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Additive);
        }

        public IEnumerator Unload(SceneAsset scene)
        {
            if (!_loadedScenes.Remove(scene))
                yield break;
            
            yield return SceneManager.UnloadSceneAsync(scene.name);
        }
    }
}