using System;
using Misc;
using UnityEngine;

namespace Asteroids
{
    public class AsteroidLayer : MonoBehaviour
    {
        public event Action<AsteroidLayer> Destroyed;
        public MaterialType Type => Material.Type;
        public float Richness { get; private set; }
        public AsteroidMaterial Material { get; private set; }
        public bool IsDestroyed { get; private set;}
        
        public void SetUp(AsteroidMaterial material, float richness)
        {
            Material = material;
            Richness = richness;
        }

        public void Destroy()
        {
            if (IsDestroyed) throw new InvalidOperationException("Already destroyed");
            IsDestroyed = true;
            Destroyed?.Invoke(this);
        }
    }
}