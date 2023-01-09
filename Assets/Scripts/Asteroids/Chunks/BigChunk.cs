using Gameplay.Interactions.Dragging;
using Gameplay.OreBoxes;
using Inventory;
using Misc;
using UnityEngine;

namespace Asteroids.Chunks
{
    public class BigChunk : MonoBehaviour, ICollectableOre, IDraggable
    {
        [SerializeField] private Rigidbody _rigidbody;
        private AsteroidMaterial _material;

        public static void Spawn(AsteroidMaterial material, Vector3 position)
        {
            BigChunk chunk = Instantiate(Resources.Load<GameObject>("BiggerChunk")).GetComponent<BigChunk>();
            chunk.transform.position = position;
            chunk._material = material;
            chunk.GetComponentInChildren<MeshRenderer>().material = material.RenderMaterial;
        }
        
        public MaterialType GetMaterialType()
        {
            return _material.Type;
        }

        public void GetCollected(PlayerInventory inventory)
        {
            if (GetMaterialType() == MaterialType.Rock)
            {
                inventory.Add(_material, 30);
            }
            else
            {
                inventory.Add(_material.BiggerChunk, 1);
            }
        }

        public Rigidbody GetRigidbody()
        {
            return _rigidbody;
        }
    }
}