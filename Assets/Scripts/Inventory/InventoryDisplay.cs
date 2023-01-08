using System;
using System.Collections.Generic;
using Misc;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryDisplay : MonoBehaviour
    {
        [SerializeField] private PlayerInventory _playerInventory; 
        [SerializeField] private VerticalLayoutGroup _inventoryItemsList;
        [SerializeField] private GameObject _inventoryItemPrefab;
        private List<InventoryItem> _inventoryItems;

        private void Awake()
        {
            _playerInventory.Changed += BuildPlayerInventoryTable;
        }

        private void OnDestroy() 
        {
            _playerInventory.Changed -= BuildPlayerInventoryTable;
        }

        private void BuildPlayerInventoryTable()
        {
            if (_inventoryItems != null) ClearItemsList();
            _inventoryItems = new List<InventoryItem>();
            foreach (AsteroidMaterial material in _playerInventory.GetMaterials())
            {
                DisplayItem(material, _playerInventory.GetAmount(material));
            }
        }

        private void ClearItemsList()
        {
            foreach (InventoryItem item in _inventoryItems)
            {
                Destroy(item.gameObject);
            }
        }

        private void DisplayItem(AsteroidMaterial material, int materialAmount)
        {
            GameObject inventoryItemGameObject = Instantiate(_inventoryItemPrefab, _inventoryItemsList.transform);
            InventoryItem item = inventoryItemGameObject.GetComponent<InventoryItem>();
            item.Display(material, materialAmount);
            _inventoryItems.Add(item);

        }

        public void ToggleInventoryShown()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}