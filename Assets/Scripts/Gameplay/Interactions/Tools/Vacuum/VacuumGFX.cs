using UnityEngine;

namespace Gameplay.Interactions.Tools
{
    public class VacuumGFX : MonoBehaviour
    {
        [SerializeField] private Vacuum _vacuum;
        [SerializeField] private Transform _cylinder;
        [SerializeField] private Transform _emptyRotation;
        [SerializeField] private Transform _fullRotation;

        private void Update()
        {
            _cylinder.localRotation = Quaternion.Slerp(_emptyRotation.localRotation, _fullRotation.localRotation, _vacuum._container.Count/(float)_vacuum._container.Capacity);
        }
    }
}