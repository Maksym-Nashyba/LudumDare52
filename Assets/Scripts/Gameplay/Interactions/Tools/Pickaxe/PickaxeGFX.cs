using System;
using Misc;
using UnityEngine;

namespace Gameplay.Interactions.Tools
{
    public class PickaxeGFX : MonoBehaviour
    {
        [SerializeField] private Pickaxe _pickaxe;
        [SerializeField] private Transform _gfxTransform;
        [SerializeField] private Transform _forwardTransform;
        [SerializeField] private Transform _rearTransform;

        private void Update()
        {
            float t = EaseFunctions.EaseOutQuart(_pickaxe.CurrentExtend);
            Vector3 lerpedPosition = Vector3.LerpUnclamped(_forwardTransform.position, _rearTransform.position, t);
            Quaternion lerpedRotation = Quaternion.LerpUnclamped(_forwardTransform.rotation, _rearTransform.rotation, t);
            _gfxTransform.SetPositionAndRotation(lerpedPosition, lerpedRotation);
        }
    }
}