using System;
using Asteroids;
using Asteroids.Chunks;
using Asteroids.Meshes;
using UnityEngine;

namespace Gameplay.Interactions.Tools
{
    public class ToolTarget : MonoBehaviour
    {
        [SerializeField] private Asteroid _asteroid;
        private ChunkPool _chunkPool;

        private void Awake()
        {
            _chunkPool = FindObjectOfType<ChunkPool>();
        }

        public void ApplyTool(Tool tool, Vector3 interactionPoint)
        {
            if (tool is Drill drill) ApplyDrill(drill, interactionPoint);
        }

        private void ApplyDrill(Drill drill, Vector3 interactionPoint)
        {
            DigHole(drill.Radius, drill.Strength, interactionPoint);
            _chunkPool.SpawnChunks(interactionPoint, 7, 
                _asteroid.GetOuterLayer().Material, _asteroid.GetOuterLayer().Richness);
        }

        private void DigHole(float radius, float strength, Vector3 position)
        {
            LayerMeshSculptor sculptor = _asteroid.GetOuterLayer().GetComponent<LayerMeshSculptor>();
            Vector3 localInteractionPoint = transform.InverseTransformPoint(position);
            localInteractionPoint /= (float)(2-_asteroid.GetOuterLayer().LayerDepth+1) / 3f;
            sculptor.CarveHole(radius, strength, localInteractionPoint);
        }
    }
}