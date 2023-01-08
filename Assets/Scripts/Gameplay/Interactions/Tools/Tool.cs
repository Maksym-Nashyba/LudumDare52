using Asteroids;
using Asteroids.Chunks;
using UnityEngine;

namespace Gameplay.Interactions.Tools
{
    public abstract class Tool : MonoBehaviour
    {
        
        protected ChunkPool ChunkPool;

        protected virtual void Awake()
        {
            ChunkPool = FindObjectOfType<ChunkPool>();
        }
        
        public abstract void Apply(Asteroid asteroid, Vector3 position);
    }
}