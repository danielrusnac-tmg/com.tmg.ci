using System;
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
            var root = new AdvancedDropdownItem("Modules");

            TypeCache.TypeCollection moduleTypes = TypeCache.GetTypesDerivedFrom<ItemModule>();

            foreach (Type moduleType in moduleTypes)
                root.AddChild(new ItemModuleDropdownItem(moduleType));

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