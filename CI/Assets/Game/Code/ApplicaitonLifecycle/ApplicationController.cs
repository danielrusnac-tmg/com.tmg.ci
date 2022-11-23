using System.Collections;
using Game.Gameplay.Messages;
using Game.Infrastructure.Game.Code.Infrastructure.PubSub;
using TMG.SceneLoader;
using TMG.ScreenFader;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Game.ApplicationLifecycle
{
    public class ApplicationController : MonoBehaviour
    {
        [SerializeField] private GameScene[] _defaultScenes;

        private bool _isRestarting;
        private ISceneLoader _sceneLoader;
        private IScreenFader _screenFader;
        private ISubscriber<QuitApplicationMessage> _quitApplicationMessage;
        private ISubscriber<RestartApplicationMessage> _restartApplicationMessage;

        [Inject]
        private void Construct(
            ISceneLoader sceneLoader, 
            IScreenFader screenFader, 
            ISubscriber<QuitApplicationMessage> quitApplicationMessage, 
            ISubscriber<RestartApplicationMessage> restartApplicationMessage)
        {
            _restartApplicationMessage = restartApplicationMessage;
            _quitApplicationMessage = quitApplicationMessage;
            _screenFader = screenFader;
            _sceneLoader = sceneLoader;
        }

        private IEnumerator Start()
        {
            _restartApplicationMessage.Subscribe(OnRestartRequest);
            _quitApplicationMessage.Subscribe(OnExitRequest);

            _screenFader.ShowCurtainImmediate();

            foreach (GameScene scene in _defaultScenes)
                yield return _sceneLoader.Load(scene);

            yield return _screenFader.HideCurtain();
        }

        private void OnDestroy()
        {
            _restartApplicationMessage.Unsubscribe(OnRestartRequest);
            _quitApplicationMessage.Unsubscribe(OnExitRequest);
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

        private void OnRestartRequest(RestartApplicationMessage message)
        {
            Restart();
        }

        private void OnExitRequest(QuitApplicationMessage message)
        {
            Exit();
        }
    }
}