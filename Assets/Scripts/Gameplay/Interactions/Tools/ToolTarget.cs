using Asteroids;
using Asteroids.Meshes;
using UnityEngine;

namespace Gameplay.Interactions.Tools
{
    public class ToolTarget : MonoBehaviour
    {
        [SerializeField] private Asteroid _asteroid;
        
        public void ApplyTool(Tool tool, Vector3 interactionPoint)
        {
            if (tool is Drill drill) ApplyDrill(drill, interactionPoint);
        }

        private void ApplyDrill(Drill drill, Vector3 interactionPoint)
        {
            LayerMeshSculptor sculptor = _asteroid.GetOuterLayer().GetComponent<LayerMeshSculptor>();
            Vector3 localInteractionPoint = transform.InverseTransformPoint(interactionPoint);
            localInteractionPoint /= (float)(2-_asteroid.GetOuterLayer().LayerDepth+1) / 3f;
            sculptor.CarveHole(0.15f, 0.2f, localInteractionPoint);
        }
    }
}