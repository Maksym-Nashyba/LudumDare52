using System;
using Inventory;
using Misc;
using UnityEngine;

namespace Gameplay.Interactions.GameplayMenus.UpgradeShop
{
    public class UpgradeStationDisplay : MenuDisplay
    {
        [SerializeField] private UpgradeStation _upgradeStation;
        [SerializeField] private PlayerInventory _inventory;

        private void Awake()
        {
            SubscribeAllMethods();
        }

        private void Start()
        {
            BuildLayoutGroupTable();
            Hide();
        }

        private void SubscribeAllMethods()
        {
            _upgradeStation.Interacted += Show;
            _upgradeStation.Closed += Hide;
            foreach (Upgrade upgrade in _upgradeStation.GetAllUpgrades())
            {
                upgrade.Crafted += BuildLayoutGroupTable;
            }
        }
        
        private void UnsubscribeAllMethods()
        {
            _upgradeStation.Interacted -= Show;
            _upgradeStation.Closed -= Hide;
            foreach (Upgrade upgrade in _upgradeStation.GetAllUpgrades())
            {
                upgrade.Crafted -= BuildLayoutGroupTable;
            }
        }

        private void OnDestroy()
        {
            UnsubscribeAllMethods();
        }

        protected override void BuildLayoutGroupTable()
        {
            base.BuildLayoutGroupTable();
            foreach (Upgrade upgrade in _upgradeStation.GetAllUpgrades())
            {
                DisplayItem(upgrade);
            }
        }

        private void DisplayItem(Upgrade upgrade)
        {
            GameObject inventoryItemGameObject = Instantiate(MenuItemPrefab, LayoutGroupTable.transform);
            UpgradeStationItem upgradeStationItem = inventoryItemGameObject.GetComponent<UpgradeStationItem>();
            if (upgrade.IsCrafted || !_inventory.HasEnoughMaterials(upgrade.Recipe))
            {
                upgradeStationItem.Display(upgrade);
                upgradeStationItem.ChangeButtonInteractability(false);
            }
            else
            {
                upgradeStationItem.Display(upgrade, (() =>
                {
                    if (!_inventory.HasEnoughMaterials(upgrade.Recipe)) return;
                    _upgradeStation.CraftUpgrade(upgrade);
                }));
                upgradeStationItem.ChangeButtonInteractability(true);
            }
            MenuItems.Add(inventoryItemGameObject);
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }
        
        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}