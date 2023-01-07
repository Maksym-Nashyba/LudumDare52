using Misc;

namespace Asteroids
{
    public class AsteroidLayer
    {
        public readonly MaterialType Type;
        public readonly float Richness;
        public bool IsDestroyed;
        
        public AsteroidLayer(MaterialType type, float richness)
        {
            Type = type;
            Richness = richness;
        }
    }
}