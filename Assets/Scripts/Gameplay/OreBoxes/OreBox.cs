using System;
using Inventory;
using Misc;
using UnityEngine;

namespace Gameplay.OreBoxes
{
    public class OreBox : MonoBehaviour
    {
        [SerializeField] private PlayerInventory _playerInventory;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody == null)return;
            if (other.attachedRigidbody.TryGetComponent(out ICollectableOre collectable))
            {
                collectable.GetCollected(_playerInventory);
            }
        }
    }
}