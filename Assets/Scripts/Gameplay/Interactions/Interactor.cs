using System;
using Asteroids;
using Gameplay.Interactions.GameplayMenus.UIPanels;
using Gameplay.Interactions.Tools;
using UnityEngine;

namespace Gameplay.Interactions
{
    public class Interactor : MonoBehaviour
    {
        public event Action Locked;
        public event Action Unlocked;
        [SerializeField] private Camera _camera;
        [SerializeField] private StartUIPanel _startPanel;
        public bool IsLocked { get; private set; }

        private void Start()
        {
            Locked?.Invoke();
            IsLocked = true;
            _startPanel.Interact(()=>
            {
                Unlocked?.Invoke();
                IsLocked = false;
            });
        }

        private void Update()
        {
            if(IsLocked)return;
            if(Input.GetKeyDown(KeyCode.E))Interact();
        }

        public void Interact()
        {
            if (!Physics.Raycast(transform.position, 
                    _camera.transform.forward, out RaycastHit hit, 5f)) return;
            if(!hit.transform.gameObject.TryGetComponent(out IInteractable interactable)) return;
            Locked?.Invoke();
            IsLocked = true;
            interactable.Interact(()=>
            {
                Unlocked?.Invoke();
                IsLocked = false;
            });
        }

        public void ApplyTool(Tool tool)
        {
            if (!Physics.Raycast(transform.position, 
                    _camera.transform.forward, out RaycastHit hit, 5f)) return;
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