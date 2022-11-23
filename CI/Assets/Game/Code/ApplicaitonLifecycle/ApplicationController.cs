using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ApplicationController
{
    public class ApplicationController : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return SceneManager.LoadSceneAsync("services", LoadSceneMode.Additive);
            yield return SceneManager.LoadSceneAsync("menu", LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("menu"));
            yield return SceneManager.LoadSceneAsync("ui", LoadSceneMode.Additive);
        }

        [ContextMenu(nameof(Restart))]
        public void Restart()
        {
            SceneManager.LoadScene("boot");
        }

        [ContextMenu(nameof(Exit))]
        public void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}