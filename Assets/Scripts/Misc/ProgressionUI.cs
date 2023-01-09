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

        private void Awake()
        {
            _inventory.BalanceChanged += OnMoneyChanged;
        }

        private void OnMoneyChanged(int balance)
        {
            _moneySlider.value = balance;
        }
    }
}