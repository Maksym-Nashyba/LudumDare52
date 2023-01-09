using System;
using UnityEngine;

namespace Misc
{
    [CreateAssetMenu(fileName = "NewUpgrade", menuName = "ScriptableObjects/Upgrades")]
    public class Upgrade : ScriptableObject
    {
        public event Action Crafted;

        public bool IsCrafted =>_isCrafted;
        [SerializeField] private bool _isCrafted;
        
        public string Name => _name;
        [SerializeField] private string _name;

        public Sprite Icon => _icon;
        [SerializeField] private Sprite _icon;

        public RecipePart[] Recipe => _recipe;
        [SerializeField] private RecipePart[] _recipe;

        public string Description => _description;
        [SerializeField] private string _description;

        public void Craft()
        {
            Crafted?.Invoke();
            _isCrafted = true;
        }
    }
}