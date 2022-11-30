using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace TMG.ModularInventory
{
    public class ItemModuleElement : VisualElement
    {
        private Action<ScriptableObject> _onRemove;
        private ScriptableObject _moduleObject;

        public ItemModuleElement(ScriptableObject moduleObject, Action<ScriptableObject> onRemove)
        {
            _moduleObject = moduleObject;
            _onRemove = onRemove;
            TemplateContainer moduleElement = ResourcesProvider.ItemModuleUxml.Instantiate();
            Add(moduleElement);
            moduleElement.Bind(new SerializedObject(moduleObject));
            
            this.Q<Button>("delete-button").clicked += OnRemoveClicked;
        }

        private void OnRemoveClicked()
        {
            _onRemove?.Invoke(_moduleObject);
        }
    }
}