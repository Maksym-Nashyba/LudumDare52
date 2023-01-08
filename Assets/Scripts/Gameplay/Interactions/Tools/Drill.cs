using Asteroids;
using Asteroids.Meshes;
using UnityEngine;

namespace Gameplay.Interactions.Tools
{
    public class Drill : Tool
    {
        public float Radius => _radius;
        [SerializeField] private float _radius;
        public float Strength => _strength;
        [SerializeField] private float _strength;

        private Interactor _interactor;

        protected override void Awake()
        {
            base.Awake();
            _interactor = FindObjectOfType<Interactor>();
        }

        private void Update()
        {
           if(Input.GetKeyDown(0)) _interactor.ApplyTool(this);
        }

        public override void Apply(Asteroid asteroid, Vector3 position)
        {
            DigHole(asteroid, Radius, Strength, position);
            ChunkPool.SpawnChunks(position, 7, 
                asteroid.GetOuterLayer().Material, asteroid.GetOuterLayer().Richness);
            asteroid.DamageOuterLayer(15);
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