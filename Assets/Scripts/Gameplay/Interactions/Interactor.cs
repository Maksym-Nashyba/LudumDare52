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

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))ApplyTool(tool);
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
            if(!hit.transform.gameObject.TryGetComponent(out ToolTarget toolTarget)) return;
            toolTarget.ApplyTool(tool, hit.point);
        }
    }
}