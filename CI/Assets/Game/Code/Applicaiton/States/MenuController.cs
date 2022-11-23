using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace Game.Application.States
{
    public class MenuController : LifetimeScope
    {
        [SerializeField] private UIDocument _uiDocument;

        private Button _restartButton;
        private Button _quitButton;
        private ApplicationController _applicationController;

        [Inject]
        private void Construct(ApplicationController applicationController)
        {
            _applicationController = applicationController;
        }

        protected override void Awake()
        {
            base.Awake();

            _restartButton = _uiDocument.rootVisualElement.Q<Button>("restart-button");
            _quitButton = _uiDocument.rootVisualElement.Q<Button>("quit-button");

            _restartButton.clicked += OnRestartPressed;
            _quitButton.clicked += OnQuitPressed;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _restartButton.clicked -= OnRestartPressed;
            _quitButton.clicked -= OnQuitPressed;
        }

        private void OnRestartPressed()
        {
            _applicationController.Restart();
        }

        private void OnQuitPressed()
        {
            _applicationController.Quit();
        }
    }
}