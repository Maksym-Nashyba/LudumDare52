using Asteroids.Meshes;
using UnityEngine;

namespace Asteroids
{
    public class AsteroidLayerGFX : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshRenderer _meshRenderer;
        private LayerMeshSculptor _meshSculptor;

        public void SetUp(LayerMeshSculptor layerMeshSculptor, Material material)
        {
            _meshSculptor = layerMeshSculptor;
            ApplyMaterial(material);
            _meshSculptor.Changed += OnMeshChanged;
        }

        private void OnDestroy()
        {
            _meshSculptor.Changed += OnMeshChanged;
        }

        private void OnMeshChanged(Mesh mesh)
        {
            _meshFilter.mesh = mesh;
        }
        
        private void ApplyMaterial(Material material)
        {
            _meshRenderer.material = material;
        }
    }
}