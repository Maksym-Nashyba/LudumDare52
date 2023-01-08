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

        }


    }
}