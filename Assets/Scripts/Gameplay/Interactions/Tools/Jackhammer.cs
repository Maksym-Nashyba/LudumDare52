using System.Net.NetworkInformation;
using Asteroids;
using Asteroids.Meshes;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Interactions.Tools
{
    public class Jackhammer : Tool
    {
        public float Radius => _radius;
        [SerializeField] private float _radius;
        public float Strength => _strength;
        [SerializeField] private float _strength;
        
        public float CurrentExtend { get; private set; }
        private Interactor _interactor;
        private float _currentSpeed;
        [SerializeField] private UnityEvent _hit;
        
        protected override void Awake()
        {
            base.Awake();
            _interactor = FindObjectOfType<Interactor>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                _currentSpeed = Mathf.Clamp01(_currentSpeed += Time.deltaTime / 1.2f);
                CurrentExtend += _currentSpeed * Time.deltaTime / 0.4f;
                if(CurrentExtend < 1f) return;
                _interactor.ApplyTool(this);
                CurrentExtend = 0;
            }
            else
            {
                _currentSpeed = Mathf.Clamp01(_currentSpeed -= Time.deltaTime / 1.2f);
            }
        }

        public override void Apply(Asteroid asteroid, Vector3 position)
        {
            if(asteroid.IsDestroyed) return;
            DigHole(asteroid, Radius, Strength, position);
            ChunkPool.SpawnChunks(position, 5, 
                asteroid.GetOuterLayer().Material, asteroid.GetOuterLayer().Richness);
            asteroid.DamageOuterLayer(8);         
            _hit?.Invoke();
        }

        private void DigHole(Asteroid asteroid, float radius, float strength, Vector3 position)
        {
            LayerMeshSculptor sculptor = asteroid.GetOuterLayer().GetComponent<LayerMeshSculptor>();
            Vector3 localInteractionPoint = asteroid.transform.InverseTransformPoint(position);
            localInteractionPoint /= (float)(2-asteroid.GetOuterLayer().LayerDepth+1) / 3f;
            sculptor.CarveHole(radius, strength, localInteractionPoint);
        }
    }
}