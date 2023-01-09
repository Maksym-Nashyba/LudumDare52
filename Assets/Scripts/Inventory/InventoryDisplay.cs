using Misc;
using UnityEngine;

namespace Inventory
{
    public class InventoryDisplay : MenuDisplay
    {
        [SerializeField] protected PlayerInventory _playerInventory;
        [SerializeField] private CanvasGroup _canvasGroup;
        private bool _shown;
        
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Tab))ToggleInventoryShown();
        }

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
            if (_shown)
            {
                _canvasGroup.alpha = 0;
                _canvasGroup.interactable = false;
            }
            else
            {
                _canvasGroup.alpha = 1;
                _canvasGroup.interactable = true;
            }
            _shown = !_shown;
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