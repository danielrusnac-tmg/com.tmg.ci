﻿using System;
using UnityEditor.IMGUI.Controls;

namespace TMG.ModularInventory
{
    internal class ItemPropertyDropdownItem : AdvancedDropdownItem
    {
        public Type Type { get; }

        public ItemPropertyDropdownItem(string name, Type type) : base(name)
        {
            Type = type;
        }
    }
}