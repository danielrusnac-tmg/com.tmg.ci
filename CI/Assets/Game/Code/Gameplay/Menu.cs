using Game.Gameplay.Messages;
using Game.Infrastructure.Game.Code.Infrastructure.PubSub;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Game.Gameplay
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;

        private Button _restartButton;
        private Button _quitButton;
        private IPublisher<QuitApplicationMessage> _quitMessage;
        private IPublisher<RestartApplicationMessage> _restartMessage;

        [Inject]
        private void Construct(IPublisher<QuitApplicationMessage> quitMessage, IPublisher<RestartApplicationMessage> restartMessage)
        {
            _restartMessage = restartMessage;
            _quitMessage = quitMessage;
        }

        private void Awake()
        {
            _restartButton = _uiDocument.rootVisualElement.Q<Button>("restart-button");
            _quitButton = _uiDocument.rootVisualElement.Q<Button>("quit-button");

            _restartButton.clicked += OnRestartPressed;
            _quitButton.clicked += OnQuitPressed;
        }

        private void OnDestroy()
        {
            _restartButton.clicked -= OnRestartPressed;
            _quitButton.clicked -= OnQuitPressed;
        }

        private void OnQuitPressed()
        {
            _quitMessage.Publish(new QuitApplicationMessage());
        }

        private void OnRestartPressed()
        {
            _restartMessage.Publish(new RestartApplicationMessage());
        }
    }
}