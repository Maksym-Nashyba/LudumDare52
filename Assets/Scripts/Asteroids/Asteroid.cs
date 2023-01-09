using System;
using System.Collections.Generic;
using Gameplay.Interactions.Dragging;
using Gameplay.OreBoxes;
using Inventory;
using Misc;
using UnityEngine;

namespace Asteroids
{
    public class Asteroid : MonoBehaviour, IDraggable, ICollectableOre
    {
        [SerializeField] private Rigidbody _rigidbody;
        public event Action Destroyed;
        public event Action LayerDestroyed;

        public Size Size => _size;
        private Size _size;
        public bool IsDestroyed { get; private set; }
        private Stack<AsteroidLayer> _asteroidLayers;

        public bool HasAnyLayers => _asteroidLayers.Count > 0;
        
        public AsteroidLayer GetOuterLayer()
        {
            if (!HasAnyLayers) throw new InvalidOperationException("There are no more layers");
            return _asteroidLayers.Peek();
        }

        public void DamageOuterLayer(int damage)
        {
            AsteroidLayer outerLayer = GetOuterLayer();
            outerLayer.Damage(damage);
            if(outerLayer.Health<=0) DestroyOuterLayer();
        }
        
        public void DestroyOuterLayer()
        {
            if (!HasAnyLayers) return;
            AsteroidLayer outerLayer = _asteroidLayers.Pop();
            outerLayer.Destroy();
            IsDestroyed = !HasAnyLayers;
            LayerDestroyed?.Invoke();
            if(IsDestroyed) Destroyed?.Invoke();
        }

        public Rigidbody GetRigidbody()
        {
            return _rigidbody;
        }

        public AsteroidMaterial GetRarestMaterial()
        {
            AsteroidLayer[] layers = _asteroidLayers.ToArray();
            AsteroidMaterial rarestMaterial = layers[0].Material;
            foreach (AsteroidLayer layer in layers)
            {
                if (layer.Material.Rarity < rarestMaterial.Rarity)
                {
                    rarestMaterial = layer.Material;
                }
            }

            return rarestMaterial;
        }

        public MaterialType GetMaterialType()
        {
            return GetOuterLayer().Material.Type;
        }

        public void GetCollected(PlayerInventory inventory)
        {
            if(_asteroidLayers.Count>1 || GetOuterLayer().Material.Type != MaterialType.Crystal) return;
            inventory.Add(GetOuterLayer().Material.LargeChunk, 1);
            Destroy(gameObject);
        }
    }
}