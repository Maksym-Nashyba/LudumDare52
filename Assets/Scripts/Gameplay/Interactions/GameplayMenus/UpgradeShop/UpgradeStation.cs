using System;
using Inventory;
using Misc;
using UnityEngine;

namespace Gameplay.Interactions.GameplayMenus.UpgradeShop
{
    public class UpgradeStation : MonoBehaviour, IInteractable
    {
        public event Action Interacted;
        public event Action Closed;

        public Upgrade DrillUpgrade => _drillUpgrade;
        [SerializeField] private Upgrade _drillUpgrade;
        
        public Upgrade VacuumUpgrade => _vacuumUpgrade;
        [SerializeField] private Upgrade _vacuumUpgrade;
        
        public Upgrade ScannerUpgrade => _scannerUpgrade;
        [SerializeField] private Upgrade _scannerUpgrade;
        
        public Upgrade PickaxeUpgrade => _pickaxeUpgrade;
        [SerializeField] private Upgrade _pickaxeUpgrade;

        [SerializeField] private PlayerInventory _inventory;
        
        private Action _closeCallback;

        public void Interact(Action closeCallback)
        {
            _closeCallback = closeCallback;
            Interacted?.Invoke();
        }

        private void Update()
        {
            if (_closeCallback == null) return;
            if (Input.GetKeyDown(KeyCode.Escape)) Close();
        }

        private void Close()
        {
            Closed?.Invoke();
            _closeCallback.Invoke();
            _closeCallback = null;
        }

        public Upgrade[] GetAllUpgrades() => new [] {_drillUpgrade, _vacuumUpgrade, _scannerUpgrade, _pickaxeUpgrade};

        public void CraftUpgrade(Upgrade upgrade)
        {
            if(upgrade.IsCrafted) return;
            upgrade.Craft();
            foreach (RecipePart recipePart in upgrade.Recipe)
            {
                _inventory.Remove(recipePart.Material, recipePart.Amount);
            }
        }
    }
}