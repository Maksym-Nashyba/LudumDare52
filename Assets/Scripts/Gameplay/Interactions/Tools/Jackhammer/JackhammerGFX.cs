using Misc;
using UnityEngine;

namespace Gameplay.Interactions.Tools
{
    public class JackhammerGFX : MonoBehaviour
    {
        [SerializeField] private Jackhammer _jackhammer;
        [SerializeField] private Transform _drillTransform;
        [SerializeField] private Transform _closeTransform;
        [SerializeField] private Transform _farTransform;

        private void Update()
        {
            _drillTransform.position = Vector3.Lerp(_closeTransform.position, _farTransform.position, EaseFunctions.EaseInCirc(_jackhammer.CurrentExtend));
        }
    }
}