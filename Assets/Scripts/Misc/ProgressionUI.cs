using System;
using Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Misc
{
    public class ProgressionUI : MonoBehaviour
    {
        [SerializeField] private Slider _timeSlider;
        [SerializeField] private Slider _moneySlider;

        [SerializeField] private PlayerInventory _inventory;
        private float _secondsPassed;
        
        private void Awake()
        {
            _inventory.BalanceChanged += OnMoneyChanged;
        }

        private void FixedUpdate()
        {
            _secondsPassed += 0.02f;
            _timeSlider.value = _secondsPassed / (15f * 60f);
        }

        private void OnMoneyChanged(int balance)
        {
            _moneySlider.value = balance;
        }
    }
}