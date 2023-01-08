using System;
using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace Asteroids
{
    public class Asteroid : MonoBehaviour
    {
        public event Action LayerDestroyed;

        public Size Size    
        {
            get => _size;
            private set => _size = value;
        }
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
        }
    }
}