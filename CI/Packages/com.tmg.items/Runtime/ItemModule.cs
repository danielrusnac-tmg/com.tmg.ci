using UnityEngine;

namespace TMG.Items
{
    public abstract class ItemModule : ScriptableObject
    {
        [SerializeField] private string _key;

        public string Key => _key;
    }
}