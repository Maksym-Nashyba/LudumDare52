using Gameplay.Interactions.Dragging;
using Gameplay.OreBoxes;
using Inventory;
using Misc;
using UnityEngine;

namespace Asteroids.Chunks
{
    public class Chunk : MonoBehaviour, IDraggable, ICollectableOre
    {
        public AsteroidMaterial Material { get; private set; }
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private Rigidbody _rigidbody;
        private ChunkPool _chunkPool;
        
        public void ApplyMaterial(AsteroidMaterial material)
        {
            Material = material;
            _renderer.material = material.RenderMaterial;
        }

        public Rigidbody GetRigidbody()
        {
            return _rigidbody;
        }

        public MaterialType GetMaterialType()
        {
            return Material.Type;
        }

        public void GetCollected(PlayerInventory inventory)
        {
            inventory.Add(Material, 1);
            _chunkPool.ReturnChunk(this);
        }

        public void BindToPool(ChunkPool pool)
        {
            _chunkPool = pool;
        }
    }
}