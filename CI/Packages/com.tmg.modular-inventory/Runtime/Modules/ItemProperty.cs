using UnityEditor;
using UnityEngine;

namespace TMG.ModularInventory.Modules
{
    public abstract class ItemModule<T> : ScriptableObject, IItemModule
    {
        [SerializeField] private string _key;
        [SerializeField] private T _value;

        public string Key => _key;

        public object Value => _value;

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (string.Equals(name, _key))
                return;

            name = _key;
            EditorUtility.SetDirty(this);
#endif
        }
    }
}