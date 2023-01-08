using System;
using UnityEngine;

namespace Gameplay.Interactions
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioSource _openDoor;
        [SerializeField] private AudioSource _closeDoor;
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
            if (!_isOpen)
            {
                _openDoor.Play();
            }
            else
            { 
                _closeDoor.Play(); 
            }
            
            _animator.Play(animationStateName);
            _isOpen = !_isOpen;
        }
    }
}