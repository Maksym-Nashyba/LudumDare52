using Misc;
using UnityEngine;

namespace Asteroids.Chunks
{
    public class Chunk : MonoBehaviour
    {
        public AsteroidMaterial Material { get; private set; }
        [SerializeField] private MeshRenderer _renderer;

        public void ApplyMaterial(AsteroidMaterial material)
        {
            Material = material;
            _renderer.material = material.RenderMaterial;
        }
    }
}