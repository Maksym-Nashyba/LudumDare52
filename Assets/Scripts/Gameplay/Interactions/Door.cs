using System;
using UnityEngine;

namespace Gameplay.Interactions
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioSource _openDoor;
        [SerializeField] private AudioSource _closeDoor;
        [SerializeField] private Workplace _workplace;
        private bool _isOpen;

        private void Awake()
        {
            _workplace.Locked += OnWorkplaceLocked;
        }

        private void OnDestroy()
        {
            _workplace.Locked -= OnWorkplaceLocked;
        }

        public void Interact(Action closeCallback)
        {
            closeCallback.Invoke();
            if(_isOpen) Close();
            else Open();
        }

        private void Open()
        {
            if(_workplace.IsLocked) return;
            _openDoor.Play();
            _animator.Play("Open");
            _isOpen = true;
        }

        private void Close()
        {
            _closeDoor.Play(); 
            _animator.Play("Close");
            _isOpen = false;
        }
        
        private void OnWorkplaceLocked()
        {
            if(_isOpen)Close();
        }
    }
}