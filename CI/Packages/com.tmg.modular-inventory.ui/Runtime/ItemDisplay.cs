using UnityEngine;
using UnityEngine.UI;

namespace TMG.ModularInventory.UI
{
    public class ItemDisplay : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        public void Display(IItem item)
        {
            _icon.sprite = item.GetModule<Sprite>("icon");
        }
    }
}