using System;
using Misc;
using UnityEngine;

namespace Asteroids
{
    public class AsteroidLayer : MonoBehaviour
    {
        public event Action<AsteroidLayer> Destroyed;
        public MaterialType Type => Material.Type;
        public AsteroidMaterial Material { get; private set; }
        public float Richness { get; private set; }
        public Depth LayerDepth { get; private set; }
        public bool IsDestroyed { get; private set;}
        
        public int Health { get; private set; }
        
        public void SetUp(AsteroidMaterial material, float richness, Depth depth)
        {
            Material = material;
            Richness = richness;
            LayerDepth = depth;
            Health = 100;
        }

        public void Damage(int damage)
        {
            Health -= Mathf.Clamp(damage, 0, int.MaxValue);
        }
        
        public void Destroy()
        {
            if (IsDestroyed) throw new InvalidOperationException("Already destroyed");
            IsDestroyed = true;
            Destroyed?.Invoke(this);
            gameObject.SetActive(false);
        }

        public enum Depth
        {
            Outer = 0,
            Middle = 1,
            Core = 2
        }
    }
}