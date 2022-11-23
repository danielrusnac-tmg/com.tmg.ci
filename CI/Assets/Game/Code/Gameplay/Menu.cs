using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Gameplay
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;

        private Button _restartButton;
        private Button _quitButton;

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

        private void OnQuitPressed() { }

        private void OnRestartPressed() { }
    }
}