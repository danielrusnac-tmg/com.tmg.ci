using System;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace TMG.ModularInventory
{
    internal class ItemModulesDropDown : AdvancedDropdown
    {
        private Action<Type> _onSelected;

        public ItemModulesDropDown(Action<Type> onSelected) : base(new AdvancedDropdownState())
        {
            _onSelected = onSelected;
            minimumSize = new Vector2(200, 400);
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            AdvancedDropdownItem root = new AdvancedDropdownItem("Modules");
            TypeCache.TypeCollection moduleTypes = TypeCache.GetTypesDerivedFrom<IItemModule>();

            foreach (Type type in moduleTypes)
            {
                if (!type.IsClass || type.IsAbstract && type.IsGenericType)
                    continue;
                
                root.AddChild(new ItemModuleDropdownItem(type.Name.Replace("Module", ""), type));
            }

            return root;
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            base.ItemSelected(item);

            if (item is ItemModuleDropdownItem dropdownItem)
                _onSelected?.Invoke(dropdownItem.Type);
        }
    }
}