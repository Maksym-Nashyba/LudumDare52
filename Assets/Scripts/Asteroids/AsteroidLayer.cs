using System;
using Asteroids.Chunks;
using Asteroids.Meshes;
using Misc;
using UnityEngine;
using Random = UnityEngine.Random;

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

        private ChunkPool _chunkPool;

        private void Awake()
        {
            _chunkPool = FindObjectOfType<ChunkPool>();
        }

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
            DropLoot();
            gameObject.SetActive(false);
        }

        private void DropLoot()
        {
            int bigChunks = (int)((3-(int)LayerDepth) * Richness);
            int smallChunks = (int)((3-(int)LayerDepth) * 50 * Richness);
            if (Type == MaterialType.Crystal) bigChunks = 0;
            for (int i = 0; i < bigChunks; i++)
            {
                BigChunk.Spawn(Material, FindPositionAroundLayer());
            }
            
            for (int i = 0; i < smallChunks; i++)
            {
                _chunkPool.SpawnChunks(FindPositionAroundLayer(), 1, Material, Richness);
            }
        }

        private Vector3 FindPositionAroundLayer()
        {
            Bounds meshBounds = GetComponent<LayerMeshSculptor>().Mesh.bounds;
            float radius = meshBounds.extents.x * (3 - (int)LayerDepth);
            return transform.position + Quaternion.Euler(Random.Range(-90f, 90), Random.Range(-90f, 90), Random.Range(-90f, 90)) * Vector3.up * radius;
        }
        
        public enum Depth
        {
            Outer = 0,
            Middle = 1,
            Core = 2
        }
    }
}