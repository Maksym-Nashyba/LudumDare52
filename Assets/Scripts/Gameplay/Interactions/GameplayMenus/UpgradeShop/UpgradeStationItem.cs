using Inventory;
using Misc;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gameplay.Interactions.GameplayMenus.UpgradeShop
{
    public class UpgradeStationItem : MonoBehaviour
    {
        [SerializeField] private Image _upgradeIcon;
        [SerializeField] private TextMeshProUGUI _upgradeName;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private VerticalLayoutGroup _materialList;
        [SerializeField] private GameObject _materialItemPrefab;
        [SerializeField] private Button _craftButton;

        public void Display(Upgrade upgrade, UnityAction action)
        {
            _upgradeIcon.sprite = upgrade.Icon; 
            _upgradeName.text = upgrade.Name;
            _description.text = upgrade.Description;
            DisplayRequiredMaterials(upgrade);
            _craftButton.onClick.AddListener(action);
        }
        
        public void Display(Upgrade upgrade)
        {
            _upgradeIcon.sprite = upgrade.Icon; 
            _upgradeName.text = upgrade.Name;
            _description.text = upgrade.Description;
            DisplayRequiredMaterials(upgrade);
        }

        public void DisplayRequiredMaterials(Upgrade upgrade)
        {
            foreach (RecipePart recipePart in upgrade.Recipe)
            {
                GameObject materialItemGameObject = Instantiate(_materialItemPrefab, _materialList.transform);
                materialItemGameObject.GetComponent<InventoryItem>().Display(recipePart.Material, recipePart.Amount);
            }
        }

        public void ChangeButtonInteractability(bool state)
        {
            _craftButton.interactable = state;
        }
    }
}