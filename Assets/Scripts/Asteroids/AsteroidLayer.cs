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
        
        public void SetUp(AsteroidMaterial material, float richness, Depth depth)
        {
            Material = material;
            Richness = richness;
            LayerDepth = depth;
        }

        public void Destroy()
        {
            if (IsDestroyed) throw new InvalidOperationException("Already destroyed");
            IsDestroyed = true;
            Destroyed?.Invoke(this);
        }

        public enum Depth
        {
            Outer = 0,
            Middle = 1,
            Core = 2
        }
    }
}