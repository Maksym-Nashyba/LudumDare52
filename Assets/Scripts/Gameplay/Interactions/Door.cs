using System;
using UnityEngine;

namespace Gameplay.Interactions
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private Animator _animator;
        private bool _isOpen;

        public void Interact(Action closeCallback)
        {
            closeCallback.Invoke();
            PlayAnimation();
        }

        private void PlayAnimation()
        {
            string animationStateName = !_isOpen ? 
                "Open"
             : "Close";
            _animator.Play(animationStateName);
            _isOpen = !_isOpen;
        }
    }
}