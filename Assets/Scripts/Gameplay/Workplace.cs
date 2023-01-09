using System;
using Asteroids;
using Asteroids.Chunks;
using UnityEngine;

namespace Gameplay
{
    public class Workplace : MonoBehaviour
    {
        public Asteroid CurrentAsteroid { get; private set; }
        public bool IsLocked { get; private set; }
        public event Action Locked;
        [SerializeField] private BoxCollider _area;
        
        public void SetNewAsteroid(Asteroid asteroid)
        {
            CurrentAsteroid = asteroid;
            asteroid.Destroyed += OnCurrentAsteroidDestroyed;
        }

        public void Lock()
        {
            Locked?.Invoke();
            IsLocked = true;
        }

        public void Unlock()
        {
            IsLocked = false;
        }
        
        public void Clear()
        {
            Collider[] allCollidersInArea = Physics.OverlapBox(_area.transform.position, _area.bounds.extents);
            ChunkPool chunkPool = FindObjectOfType<ChunkPool>();
            foreach (Collider collider in allCollidersInArea)
            {
                if (collider.TryGetComponent(out Chunk chunk))
                {
                    chunkPool.ReturnChunk(chunk);
                }else if (collider.TryGetComponent(out Asteroid asteroid))
                {
                    Destroy(asteroid.gameObject);
                }
            }
        }
        
        private void OnCurrentAsteroidDestroyed()
        {
            CurrentAsteroid = null;
        }
    }
}