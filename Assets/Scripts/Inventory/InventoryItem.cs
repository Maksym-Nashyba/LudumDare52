using Misc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private Image _materialIcon;
        [SerializeField] private TextMeshProUGUI _materialName;
        [SerializeField] private TextMeshProUGUI _materialAmount;

        public void Display(AsteroidMaterial material, int materialAmount)
        {
            //_materialIcon.sprite = material.Icon; //TODO display material icon
            _materialName.text = material.Name;
            _materialAmount.text = $"Amount: {materialAmount}";
        }
    }
}