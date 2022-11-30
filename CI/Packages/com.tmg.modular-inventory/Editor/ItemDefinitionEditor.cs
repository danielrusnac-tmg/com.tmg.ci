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

            RefreshModuleElements();
            
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
        private void AddModule(ScriptableObject property)
        {
            _item.Modules.Add(property);
            AssetDatabase.AddObjectToAsset(property, _item);
            EditorUtility.SetDirty(_item);
            AssetDatabase.SaveAssetIfDirty(_item);

            // RefreshProperties();
        }

        private void RemoveProperty(ScriptableObject property)
        {
            _item.Modules.Remove(property);
            AssetDatabase.RemoveObjectFromAsset(property);
            EditorUtility.SetDirty(_item);
            AssetDatabase.SaveAssetIfDirty(_item);

            // RefreshProperties();
        }

        private void RefreshModuleElements()
        {
            // int childCount = _content.childCount;

            // for (int i = childCount - 1; i >= 0; i--)
                // _content.RemoveAt(i);

                foreach (ScriptableObject module in _item.Modules)
                {
                    _content.Add(new ItemModuleElement(module, RemoveProperty));
                    // _content.Add(new InspectorElement(property));
                }        
        }
    }
}