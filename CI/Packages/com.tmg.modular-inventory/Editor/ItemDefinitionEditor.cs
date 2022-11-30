using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace TMG.ModularInventory
{
    [CustomEditor(typeof(ItemDefinition))]
    public class ItemDefinitionEditor : Editor
    {
        private ItemDefinition _item;
        private VisualElement _content;
        private Dictionary<ScriptableObject, VisualElement> _elementByModule = new Dictionary<ScriptableObject, VisualElement>();

        private void OnEnable()
        {
            _item = (ItemDefinition) target;
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualTreeAsset visualTree = ResourcesProvider.ItemDefinitionUxml;
            VisualElement root = visualTree.CloneTree();

            _content = root.Q<VisualElement>("content");
            root.Q<Label>("id").text = _item.ID;
            root.Q<Button>("add-button").clicked += OnAddModuleClicked;

            foreach (ScriptableObject module in _item.Modules)
                AddModuleElement(module);
            
            return root;
        }

        private void OnAddModuleClicked()
        {
            Rect rect = new Rect(Event.current.mousePosition, new Vector2(0, 0));
            ItemModulesDropDown dropdown = new ItemModulesDropDown(AddModule);
            dropdown.Show(rect);
        }

        private void AddModule(Type type)
        {
            AddModule(CreateInstance(type));
        }
        private void AddModule(ScriptableObject module)
        {
            _item.Modules.Add(module);
            
            AssetDatabase.AddObjectToAsset(module, _item);
            EditorUtility.SetDirty(_item);
            AssetDatabase.SaveAssetIfDirty(_item);
            
            AddModuleElement(module);
        }

        private void RemoveModule(ScriptableObject module)
        {
            RemoveModuleElement(module);
            _item.Modules.Remove(module);

            AssetDatabase.RemoveObjectFromAsset(module);
            EditorUtility.SetDirty(_item);
            AssetDatabase.SaveAssetIfDirty(_item);
        }

        private void AddModuleElement(ScriptableObject module)
        {
            ItemModuleElement element = new ItemModuleElement(module, RemoveModule);

            _elementByModule.Add(module, element);
            _content.Add(element);
        }
        
        private void RemoveModuleElement(ScriptableObject module)
        {
            if (!_elementByModule.ContainsKey(module))
                return;
            
            _content.Remove(_elementByModule[module]);
            _elementByModule.Remove(module);
        }
    }
}