using System.Collections;
using TMG.SceneLoader;
using TMG.ScreenFader;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Game.ApplicationLifecycle
{
    public class ApplicationController : LifetimeScope
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private UssScreenFader _screenFader;
        [SerializeField] private GameScene[] _defaultScenes;

        private bool _isRestarting;
        
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
            if (_isRestarting)
                return;

            StartCoroutine(RestartRoutine());
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

        private IEnumerator RestartRoutine()
        {
            _isRestarting = true;
            yield return _screenFader.ShowCurtain();
            SceneManager.LoadScene(0);
        }
    }
}