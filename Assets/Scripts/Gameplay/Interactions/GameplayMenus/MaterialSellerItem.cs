using System;
using Inventory;
using Misc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Interactions.GameplayMenus
{
    public class MaterialSellerItem : InventoryItem
    {
        [SerializeField] private TextMeshProUGUI _materialPrice;
        public int SellAmount => Convert.ToInt32(_sellAmountInputField.text);
        [SerializeField] private TMP_InputField _sellAmountInputField;
        [SerializeField] public Button SellButton;

        public override void Display(AsteroidMaterial material, int materialAmount)
        {
            base.Display(material, materialAmount);
            _materialPrice.text = $"{material.Price}$";
            _sellAmountInputField.placeholder.GetComponent<TextMeshProUGUI>().text = $"0 - {materialAmount}";
        }

        public void CheckInputFieldValue()
        {
            try
            {
                int inputFieldValue = Convert.ToInt32(_sellAmountInputField.text);
                int materialAmount = Convert.ToInt32(_materialAmount.text);
                
                if (inputFieldValue > materialAmount)
                {
                    _sellAmountInputField.text = _materialAmount.text;
                }
                else if (inputFieldValue < 0)
                {
                    _sellAmountInputField.text = 0.ToString();
                }
            }
            catch (Exception e)
            {
                _sellAmountInputField.text = "0";
                Console.WriteLine(e);
            }
        }
    }
}