using System;

namespace TMG.ModularInventory
{
    [Serializable]
    public struct InventoryData
    {
        public string[] IDs;
        public int[] Amounts;
        public int[] Limits;

        public bool IsValid => IDs != null && IDs.Length > 0;
    }
}