using System;
using System.Collections.Generic;
using Gameplay.Interactions.Dragging;
using Misc;
using UnityEngine;

namespace Asteroids
{
    public class Asteroid : MonoBehaviour, IDraggable
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
    }
}