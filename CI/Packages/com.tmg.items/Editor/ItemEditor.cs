using System;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace TMG.Items
{
    [CustomEditor(typeof(Item))]
    internal class ItemEditor : Editor
    {
        private Item _item;
        private VisualElement _modulesElement;

        private void OnEnable()
        {
            _item = (Item) target;
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            _modulesElement = new VisualElement();
            RefreshModules();

            root.Add(new PropertyField(serializedObject.FindProperty("_id")));
            root.Add(_modulesElement);
            root.Add(new Button(OnAddModuleClicked) {text = "Add"});
            root.Add(new Button(OnRemoveModuleClicked) {text = "Remove"});
            return root;
        }

        private void OnAddModuleClicked()
        {
            Rect rect = new Rect(Event.current.mousePosition, new Vector2(0, 0));
            ItemModuleDropDown dropdown = new ItemModuleDropDown(AddModule);
            dropdown.Show(rect);
        }

        private void OnRemoveModuleClicked()
        {
            if (!_item.Modules.Any())
                return;

            ItemModule module = _item.Modules.Last();
            RemoveModule(module);
        }
        
        private void AddModule(Type type)
        {
            ScriptableObject module = CreateInstance(type);
            
            if (module != null && module is ItemModule itemModule)
                AddModule(itemModule);
        }

        private void AddModule<T>() where T : ItemModule
        {
            T module = CreateInstance<T>();
            AddModule(module);
        }

        private void AddModule(ItemModule module)
        {
            _item.Modules.Add(module);
            AssetDatabase.AddObjectToAsset(module, _item);
            EditorUtility.SetDirty(_item);
            AssetDatabase.SaveAssetIfDirty(_item);

            RefreshModules();
        }

        private void RemoveModule(ItemModule module)
        {
            _item.Modules.Remove(module);
            AssetDatabase.RemoveObjectFromAsset(module);
            EditorUtility.SetDirty(_item);
            AssetDatabase.SaveAssetIfDirty(_item);

            RefreshModules();
        }

        private void RefreshModules()
        {
            int childCount = _modulesElement.childCount;

            for (int i = childCount - 1; i >= 0; i--)
                _modulesElement.RemoveAt(i);

            foreach (ItemModule module in _item.Modules)
            {
                _modulesElement.Add(new InspectorElement(module));
            }
        }
    }
}