using UnityEngine;

namespace Gameplay.Interactions
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        public void Interact()
        {
            if (!Physics.Raycast(transform.position, 
                    _camera.transform.forward, out RaycastHit hit, 25f)) return;
            if(!hit.transform.gameObject.TryGetComponent(out IInteractable interactable)) return;
            interactable.Interact();
        }
    }
}