using System;
using Misc;

namespace Asteroids
{
    public class AsteroidLayer
    {
        public MaterialType Type => Material.Type;
        public readonly float Richness;
        public readonly AsteroidMaterial Material;
        public bool IsDestroyed { get; private set;}
        
        public AsteroidLayer(AsteroidMaterial material, float richness)
        {
            Material = material;
            Richness = richness;
        }

        public void Destroy()
        {
            if (IsDestroyed) throw new InvalidOperationException("Already destroyed");
            IsDestroyed = true;
        }
    }
}