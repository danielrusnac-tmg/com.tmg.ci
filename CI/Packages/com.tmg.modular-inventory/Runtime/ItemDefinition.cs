using System.Collections.Generic;
using UnityEngine;

namespace TMG.ModularInventory
{
    [CreateAssetMenu(menuName = "Inventories/Item", fileName = "item_")]
    public class ItemDefinition : ScriptableObject, IItem
    {
        [SerializeField] private List<ScriptableObject> _modules = new List<ScriptableObject>();

        public List<ScriptableObject> Modules => _modules;

        public string ID => name;

        public T GetModule<T>(string key)
        {
            TryGetModule(key, out T property);
            return property;
        }

        public bool TryGetModule<T>(string key, out T module)
        {
            foreach (ScriptableObject soModule in Modules)
            {
                if (soModule is IItemModule itemModule &&
                    string.Equals(itemModule.Key, key) &&
                    itemModule.Value is T castedModule)
                {
                    module = castedModule;
                    return true;
                }
            }

            module = default;
            return false;
        }
    }
}