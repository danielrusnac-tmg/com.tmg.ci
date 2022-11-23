using UnityEngine;
using UnityEngine.UIElements;

public class UIPreprocessorDisplay : MonoBehaviour
{
    [SerializeField] private UIDocument _uiDocument;

    private void Start()
    {
        DisplayPreprocessors();
    }

    private void DisplayPreprocessors()
    {
        // _uiDocument.rootVisualElement.Q<Label>("preprocessors-label").text;
    }
}