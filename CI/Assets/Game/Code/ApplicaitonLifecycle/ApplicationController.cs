using System.Collections;
using TMG.SceneLoader;
using TMG.ScreenFader;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ApplicationController
{
    public class ApplicationController : LifetimeScope
    {
        [SerializeField] private GameScene[] _defaultScenes;
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private UssScreenFader _screenFader;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance(_sceneLoader).AsImplementedInterfaces();
            builder.RegisterInstance(_screenFader).AsImplementedInterfaces();
        }

        private IEnumerator Start()
        {
            _screenFader.ShowCurtainImmediate();
            
            foreach (GameScene scene in _defaultScenes)
                yield return _sceneLoader.Load(scene);

            yield return _screenFader.HideCurtain();
        }

        [ContextMenu(nameof(Restart))]
        public void Restart()
        {
            SceneManager.LoadScene(0);
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