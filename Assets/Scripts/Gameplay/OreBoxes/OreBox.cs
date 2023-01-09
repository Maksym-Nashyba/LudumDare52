using System;
using Inventory;
using Misc;
using UnityEngine;

namespace Gameplay.OreBoxes
{
    public class OreBox : MonoBehaviour
    {
        [SerializeField] private MaterialType _materialType;
        [SerializeField] private PlayerInventory _playerInventory;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollectableOre collectable))
            {
                if(collectable.GetMaterialType() == _materialType)collectable.GetCollected(_playerInventory);
            }
        }
    }
}