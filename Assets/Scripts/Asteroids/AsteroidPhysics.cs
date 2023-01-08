using Asteroids.Meshes;
using UnityEngine;

namespace Asteroids
{
    public class AsteroidPhysics : MonoBehaviour
    {
        [SerializeField] private MeshCollider _collider;
        private Asteroid _asteroid;
        private LayerMeshSculptor _currentMeshSculptor;
        
        public void SetUp(Asteroid asteroid)
        {
            _asteroid = asteroid;
            _asteroid.LayerDestroyed += OnLayerDestroyed;
            SwitchToNextMeshSource();
        }

        private void OnDestroy()
        {
            _asteroid.LayerDestroyed -= OnLayerDestroyed;
        }

        private void OnLayerDestroyed()
        {
            if(_asteroid.IsDestroyed) return;
            SwitchToNextMeshSource();
        }

        private void SwitchToNextMeshSource()
        {
            _currentMeshSculptor = FindNextSculptor();
            _collider.sharedMesh = _currentMeshSculptor.Mesh;
            float scale = (float)(2-_asteroid.GetOuterLayer().LayerDepth+1) / 3f;
            _collider.transform.localScale = new Vector3(scale, scale, scale);
            _collider.transform.localPosition = Vector3.zero;
            _collider.transform.localRotation = Quaternion.identity;
        }
    
        private LayerMeshSculptor FindNextSculptor()
        {
            return _asteroid.GetOuterLayer().GetComponent<LayerMeshSculptor>();
        }
    }
}