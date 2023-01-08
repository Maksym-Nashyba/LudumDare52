using Gameplay.Interactions.Dragging;
using Misc;
using UnityEngine;

namespace Asteroids.Chunks
{
    public class Chunk : MonoBehaviour, IDraggable
    {
        public AsteroidMaterial Material { get; private set; }
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private Rigidbody _rigidbody;

        public void ApplyMaterial(AsteroidMaterial material)
        {
            Material = material;
            _renderer.material = material.RenderMaterial;
        }

        public Rigidbody GetRigidbody()
        {
            return _rigidbody;
        }
    }
}