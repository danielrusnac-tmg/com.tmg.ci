using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TMG.ModularInventory
{
    [CreateAssetMenu(menuName = "Inventories/Module ID Database", fileName = "moduleIdDatabase")]
    public class ModuleIdDatabase : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private ModuleNameID[] _moduleyNames = Array.Empty<ModuleNameID>();

        private static Dictionary<int, string> s_nameById = new Dictionary<int, string>();

        public static string GetModuleName(int id)
        {
            return s_nameById.ContainsKey(id) 
                ? string.Empty 
                : s_nameById[id];
        }

        public void OnBeforeSerialize()
        {
            _moduleyNames = s_nameById.Select(pair => new ModuleNameID(pair.Key, pair.Value)).ToArray();
        }

        public void OnAfterDeserialize()
        {
            s_nameById = _moduleyNames.ToDictionary(moduleName => moduleName.ID, moduleName => moduleName.Name);
        }

        [Serializable]
        private struct ModuleNameID
        {
            public int ID;
            public string Name;

            public ModuleNameID(int id, string name)
            {
                ID = id;
                Name = name;
            }
        }
    }
}