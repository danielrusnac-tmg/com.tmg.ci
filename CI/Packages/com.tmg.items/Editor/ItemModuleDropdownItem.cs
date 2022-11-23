using System;
using UnityEditor.IMGUI.Controls;

namespace TMG.Items
{
    internal class ItemModuleDropdownItem : AdvancedDropdownItem
    {
        public Type Type { get; }

        public ItemModuleDropdownItem(Type type) : base(type.Name.Replace("ItemModule", ""))
        {
            Type = type;
        }
    }
}