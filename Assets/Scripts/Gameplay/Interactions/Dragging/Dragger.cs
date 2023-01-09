using UnityEngine;

namespace Gameplay.Interactions.Dragging
{
    public class Dragger : MonoBehaviour
    {
        [SerializeField] private Transform _dragPoint;
        [SerializeField] private Interactor _interactor;
        [SerializeField] private float _force;
        private Rigidbody _draggedObject;

        private void Update()
        {
            if (_draggedObject == null)
            {
                if (Input.GetKeyDown(KeyCode.E)) _draggedObject = TryPickUp();
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.E)) Drop();
            }
        }

        private void FixedUpdate()
        {
            if (_draggedObject == null) return;
            Drag(_draggedObject);
        }

        private Rigidbody TryPickUp()
        {
            Transform hitTransform =_interactor.RaycastSearch<IDraggable>(25f);
            if (hitTransform == null) return null;
            return hitTransform.GetComponent<IDraggable>().GetRigidbody();
        }
        
        private void Drag(Rigidbody draggedObject)
        {
            draggedObject.AddForce((_dragPoint.position - draggedObject.position).normalized * _force, ForceMode.Force);
            draggedObject.velocity *= Mathf.Clamp01((_dragPoint.position - draggedObject.position).magnitude);
        }

        private void Drop()
        {
            _draggedObject = null;
        }
    }
}