using System;
using Asteroids;
using Asteroids.Chunks;
using Asteroids.Meshes;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Interactions.Tools
{
    public class Pickaxe : Tool
    {
        public UnityEvent Hit;
        public float CurrentExtend { get; private set; }
        [SerializeField] private Interactor _interactor;
        [SerializeField] private float _extendTimeSeconds;
        public float Radius => _radius;
        [SerializeField] private float _radius;
        public float Strength => _strength;
        [SerializeField] private float _strength;

        private void Update()
        {
            if (!_interactor.IsLocked && Input.GetMouseButton(0))
            {
                CurrentExtend = Mathf.Clamp01(CurrentExtend += Time.deltaTime / _extendTimeSeconds);
            }
            else
            {
                CurrentExtend = Mathf.Clamp01(CurrentExtend -= Time.deltaTime / _extendTimeSeconds);
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (CurrentExtend >= 0.95f)
                {
                    _interactor.ApplyTool(this);
                    CurrentExtend = 0f;
                }
            }
        }

        public override void Apply(Asteroid asteroid, Vector3 position)
        {
            DigHole(asteroid, Radius, Strength, position);
            BigChunk.Spawn(asteroid.GetOuterLayer().Material, position);
            asteroid.DamageOuterLayer(30);
            Hit?.Invoke();
        }
        
        private void DigHole(Asteroid asteroid, float radius, float strength, Vector3 position)
        {
            LayerMeshSculptor sculptor = asteroid.GetOuterLayer().GetComponent<LayerMeshSculptor>();
            Vector3 localInteractionPoint = asteroid.transform.InverseTransformPoint(position);
            localInteractionPoint /= (float)(3-asteroid.GetOuterLayer().LayerDepth) / 3f;
            sculptor.CarveHole(radius, strength, localInteractionPoint);
        }
    }
}