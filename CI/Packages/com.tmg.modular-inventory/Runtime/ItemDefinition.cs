using System.Collections.Generic;
using UnityEngine;

namespace TMG.ModularInventory
{
    [CreateAssetMenu(menuName = "Inventories/Item", fileName = "item_")]
    public class ItemDefinition : ScriptableObject, IItem
    {
        [SerializeField] private List<ScriptableObject> _properties = new List<ScriptableObject>();

        public List<ScriptableObject> Properties => _properties;

        public string ID => name;

        public T GetModule<T>(string key)
        {
            TryGetModule(key, out T property);
            return property;
        }

        public bool TryGetModule<T>(string key, out T module)
        {
            foreach (ScriptableObject so in Properties)
            {
                if (so is IItemModule ItemModule &&
                    string.Equals(ItemModule.Key, key) &&
                    ItemModule.Value is T castedProperty)
                {
                    module = castedProperty;
                    return true;
                }
            }

            module = default;
            return false;
        }
    }
}