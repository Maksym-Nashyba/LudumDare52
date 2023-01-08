using Misc;
using UnityEngine;

namespace Inventory
{
    public class InventoryDisplay : MenuDisplay
    {
        [SerializeField] protected PlayerInventory _playerInventory;

        protected virtual void Awake()
        {
            _playerInventory.Changed += BuildLayoutGroupTable;
        }

        protected virtual void OnDestroy() 
        {
            _playerInventory.Changed -= BuildLayoutGroupTable;
        }

        public void ToggleInventoryShown()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
        
        protected override void BuildLayoutGroupTable()
        {
            base.BuildLayoutGroupTable();
            foreach (AsteroidMaterial material in _playerInventory.GetMaterials())
            {
                DisplayItem(material, _playerInventory.GetAmount(material));
            }
        }

        protected virtual void DisplayItem(AsteroidMaterial material, int materialAmount)
        {
            GameObject inventoryItemGameObject = Instantiate(MenuItemPrefab, LayoutGroupTable.transform);
            inventoryItemGameObject.GetComponent<InventoryItem>().Display(material, materialAmount);
            MenuItems.Add(inventoryItemGameObject);
        }
    }
}