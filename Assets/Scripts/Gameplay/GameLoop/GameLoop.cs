using System;
using System.Threading;
using System.Threading.Tasks;
using Inventory;
using UnityEngine;

namespace Gameplay.GameLoop
{
    public class GameLoop : MonoBehaviour
    {
        public event Action Started;
        public event Action<bool> Ended;
        [SerializeField] private PlayerInventory _playerInventory;
        private CancellationTokenSource _cancellationTokenSource; 
        
        private void Awake()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _playerInventory.BalanceChanged += OnBalanceChanged;
        }

        private async void Start()
        {
            Started?.Invoke();
            await Task.Delay(15 * 60 * 1000);
            if(_cancellationTokenSource.Token.IsCancellationRequested)return;
            OnTimeIsOver();
        }

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
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