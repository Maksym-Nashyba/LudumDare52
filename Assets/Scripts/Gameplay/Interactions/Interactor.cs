using System;
using Asteroids;
using Gameplay.Interactions.Tools;
using UnityEngine;

namespace Gameplay.Interactions
{
    public class Interactor : MonoBehaviour
    {
        public event Action Locked;
        public event Action Unlocked;
        [SerializeField] private Camera _camera;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E)) Interact();
        }

        public void Interact()
        {
            if (!Physics.Raycast(transform.position, 
                    _camera.transform.forward, out RaycastHit hit, 25f)) return;
            if(!hit.transform.gameObject.TryGetComponent(out IInteractable interactable)) return;
            interactable.Interact(()=>Unlocked?.Invoke());
            Locked?.Invoke();
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
                    _camera.transform.forward, out RaycastHit hit, range)) return null;
            if(!hit.transform.gameObject.TryGetComponent(out T searched)) return null;
            return hit.transform;
        }
    }
}