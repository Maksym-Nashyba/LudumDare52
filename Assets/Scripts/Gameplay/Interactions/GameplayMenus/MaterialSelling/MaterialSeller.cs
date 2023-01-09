using System;
using Inventory;
using Misc;
using UnityEngine;

namespace Gameplay.Interactions.GameplayMenus.MaterialSelling
{
    public class MaterialSeller : MonoBehaviour, IInteractable
    {
        public event Action Interacted;
        [SerializeField] private MaterialSellerDisplay _display;
        [SerializeField] private PlayerInventory _playerInventory;
        public Action CloseCallback;

        public void Interact(Action closeCallback)
        {
            CloseCallback = closeCallback;
            _display.Show();
            Interacted?.Invoke();
        }

        public void Hide()
        {
            _display.Hide();
            CloseCallback.Invoke();
        }

        public void SellMaterial(AsteroidMaterial material, int sellAmount)
        {
            _playerInventory.Remove(material, sellAmount);
            _playerInventory.AddMoney(material.Price * sellAmount);
        }
    }
}