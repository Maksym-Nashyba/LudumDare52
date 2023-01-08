using Asteroids;
using Gameplay.Interactions.Tools;
using UnityEngine;

namespace Gameplay.Interactions
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        private Tool tool;

        private void Awake()
        {
            tool = GetComponent<Tool>();
        }
        
        public void Interact()
        {
            if (!Physics.Raycast(transform.position, 
                    _camera.transform.forward, out RaycastHit hit, 25f)) return;
            if(!hit.transform.gameObject.TryGetComponent(out IInteractable interactable)) return;
            interactable.Interact();
        }

        public void ApplyTool(Tool tool)
        {
            if (!Physics.Raycast(transform.position, 
                    _camera.transform.forward, out RaycastHit hit, 25f)) return;
            if(!hit.transform.gameObject.TryGetComponent(out Asteroid asteroid)) return;
            tool.Apply(asteroid, hit.point);
        }

        public Transform RaycastSearch<T>(float range)
        {
            if (!Physics.Raycast(transform.position, 
                    _camera.transform.forward, out RaycastHit hit, 25f)) return null;
            if(!hit.transform.gameObject.TryGetComponent(out T searched)) return null;
            return hit.transform;
        }
    }
}