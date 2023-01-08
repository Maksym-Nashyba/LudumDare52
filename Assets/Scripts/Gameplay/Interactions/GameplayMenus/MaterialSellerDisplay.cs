using Inventory;
using Misc;
using UnityEngine;

namespace Gameplay.Interactions.GameplayMenus
{
    public class MaterialSellerDisplay : InventoryDisplay
    {
        [SerializeField] private MaterialSeller _materialSeller;

        protected override void Awake()
        {
            base.Awake();
            _materialSeller.Interacted += BuildLayoutGroupTable;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _materialSeller.Interacted -= BuildLayoutGroupTable;
        }

        protected override void DisplayItem(AsteroidMaterial material, int materialAmount)
        {
            GameObject inventoryItemGameObject = Instantiate(MenuItemPrefab, LayoutGroupTable.transform);
            MaterialSellerItem sellerItem = inventoryItemGameObject.GetComponent<MaterialSellerItem>();
            sellerItem.Display(material, materialAmount);
            sellerItem.SellButton.onClick.AddListener(() =>
            {
                sellerItem.CheckInputFieldValue();
                _materialSeller.SellMaterial(material, sellerItem.SellAmount);
            });
            MenuItems.Add(inventoryItemGameObject);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}