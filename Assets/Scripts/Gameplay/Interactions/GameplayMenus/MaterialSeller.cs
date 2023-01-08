using System;
using Inventory;
using Misc;
using UnityEngine;

namespace Gameplay.Interactions.GameplayMenus
{
    public class MaterialSeller : MonoBehaviour, IInteractable
    {
        [SerializeField] private MaterialSellerDisplay _display;
        [SerializeField] private PlayerInventory _playerInventory;
        private Action _closeCallback;

        public void Interact(Action closeCallback)
        {
            _closeCallback = closeCallback;
            _display.Show();
        }

        public void Hide()
        {
            _display.Hide();
            _closeCallback.Invoke();
        }

        public void SellMaterial(AsteroidMaterial material, int sellAmount)
        {
            _playerInventory.Remove(material, sellAmount);
            _playerInventory.AddMoney(material.Price * sellAmount);
        }
    }
}