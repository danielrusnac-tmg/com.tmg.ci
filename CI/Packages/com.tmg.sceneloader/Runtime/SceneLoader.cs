using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TMG.SceneLoader
{
    public class SceneLoader : MonoBehaviour, ISceneLoader
    {
        private HashSet<GameScene> _loadedScenes = new();

        public bool IsLoaded(GameScene scene) => _loadedScenes.Contains(scene);

        public IEnumerator Load(GameScene scene)
        {
            if (!_loadedScenes.Add(scene))
                yield break;

            yield return SceneManager.LoadSceneAsync(scene.SceneName, LoadSceneMode.Additive);
        }

        public IEnumerator Unload(GameScene scene)
        {
            if (!_loadedScenes.Remove(scene))
                yield break;

            yield return SceneManager.UnloadSceneAsync(scene.SceneName);
        }
    }
}