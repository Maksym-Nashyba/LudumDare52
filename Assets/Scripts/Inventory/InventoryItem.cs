using Misc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] protected Image _materialIcon;
        [SerializeField] protected TextMeshProUGUI _materialName;
        [SerializeField] protected TextMeshProUGUI _materialAmount;

        public virtual void Display(AsteroidMaterial material, int materialAmount)
        {
            _materialIcon.sprite = material.Sprite;
            _materialName.text = material.Name;
            _materialAmount.text = materialAmount.ToString();
        }
    }
}