using System;
using UnityEditor.IMGUI.Controls;

namespace TMG.ModularInventory
{
    internal class ItemModuleDropdownItem : AdvancedDropdownItem
    {
        public Type Type { get; }

        public ItemModuleDropdownItem(string name, Type type) : base(name)
        {
            Type = type;
        }
    }
}