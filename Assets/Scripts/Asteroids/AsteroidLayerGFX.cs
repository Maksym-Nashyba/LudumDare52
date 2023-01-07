using Asteroids.Meshes;
using UnityEngine;

namespace Asteroids
{
    public class AsteroidLayerGFX : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshRenderer _meshRenderer;
        private LayerMeshSculptor _meshSculptor;

        public void SetUp(LayerMeshSculptor layerMeshSculptor, AsteroidLayer layer)
        {
            _meshSculptor = layerMeshSculptor;
            ApplyMaterial(layer.Material.RenderMaterial);
            _meshSculptor.Changed += OnMeshChanged;
            _meshFilter.mesh = layerMeshSculptor.Mesh;
            SetSize01((1+(2-(int)layer.LayerDepth))/3f);
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

        private void SetSize01(float size01)
        {
            _meshRenderer.transform.localScale = new Vector3(size01, size01, size01);
        }
    }
}