using System;
using Inventory;
using UnityEngine;

namespace Gameplay.GameLoop
{
    public class GameLoop : MonoBehaviour
    {
        public event Action Started;
        public event Action<bool> Ended;
        [SerializeField] private PlayerInventory _playerInventory;

        private void Awake()
        {
            _playerInventory.BalanceChanged += OnBalanceChanged;
        }

        private void Start()
        {
            Started?.Invoke();
        }

        private void OnDestroy()
        {
            _playerInventory.BalanceChanged += OnBalanceChanged;
        }

        private void OnBalanceChanged(int balance)
        {
            if(balance < 1_000_000) return;
            Ended?.Invoke(true);
        }

        private void OnTimeIsOver()
        {
            bool success = _playerInventory.GetBalance() >= 1_000_000;
            Ended?.Invoke(success);
        }
    }
}