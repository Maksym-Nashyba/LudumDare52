using Misc;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryDisplay : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory; 
        [SerializeField] private VerticalLayoutGroup _inventoryItemsList;
        [SerializeField] private GameObject _inventoryItemPrefab;

        private void Awake()
        {
            _inventory.Changed += BuildInventoryTable;
        }

        private void Start()
        {
            BuildInventoryTable();
        }
        
        private void OnDestroy() 
        {
            _inventory.Changed -= BuildInventoryTable;
        }

        private void BuildInventoryTable()
        {
            foreach (AsteroidMaterial material in _inventory.GetMaterials())
            {
                DisplayItem(material, _inventory.GetAmount(material));
            }
        }

        private void DisplayItem(AsteroidMaterial material, int materialAmount)
        {
            GameObject inventoryItemGameObject = Instantiate(_inventoryItemPrefab, _inventoryItemsList.transform);
            inventoryItemGameObject.GetComponent<InventoryItem>().Display(material, materialAmount);
        }

        public void ToggleInventoryShown()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}