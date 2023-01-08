using System;
using UnityEngine;

namespace Gameplay.Interactions
{
    public class AsteroidCatcher : MonoBehaviour, IInteractable
    {
        private Action _closeCallback;
        
        public void Interact(Action closeCallback)
        {
            _closeCallback = closeCallback;
        }

        private void Update()
        {
            if (_closeCallback == null) return;
            if (Input.GetKeyDown(KeyCode.Escape)) Close();
        }

        private void Close()
        {
            _closeCallback.Invoke();
            _closeCallback = null;
        }
    }
}