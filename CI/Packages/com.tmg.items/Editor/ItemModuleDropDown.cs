using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace TMG.Items
{
    internal class ItemModuleDropDown : AdvancedDropdown
    {
        private Action<Type> _onSelected;

        public ItemModuleDropDown(Action<Type> onSelected) : base(new AdvancedDropdownState())
        {
            _onSelected = onSelected;
            minimumSize = new Vector2(200, 400);
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            AdvancedDropdownItem root = new AdvancedDropdownItem("Modules");
            TypeCache.TypeCollection moduleTypes = TypeCache.GetTypesDerivedFrom<ItemModule>();

            foreach (Type moduleType in moduleTypes)
            {
                if (moduleType.IsAbstract && moduleType.IsGenericType)
                    continue;
                
                root.AddChild(new ItemModuleDropdownItem(moduleType.Name.Replace("ItemModule", ""), moduleType));
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