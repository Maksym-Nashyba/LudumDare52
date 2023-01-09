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
        [SerializeField] private EndGameScreen _winScreen;
        [SerializeField] private EndGameScreen _lossScreen;
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

            FindObjectOfType<GameLoop.GameLoop>().Ended += OnGameEnded;
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
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
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

        private void OnGameEnded(bool result)
        {
            if(result) ShowWinScreen();
            else ShowLossScreen();
        }
        
        public void ShowWinScreen()
        {
            Locked?.Invoke();
            IsLocked = true;
            _winScreen.Interact(()=>
            {
                Unlocked?.Invoke();
                IsLocked = false;
            });
        }
        
        public void ShowLossScreen()
        {
            Locked?.Invoke();
            IsLocked = true;
            _lossScreen.Interact(()=>
            {
                Unlocked?.Invoke();
                IsLocked = false;
            });
        }
    }
}