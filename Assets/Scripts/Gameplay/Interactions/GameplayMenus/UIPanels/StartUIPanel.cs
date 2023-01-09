using System;
using UnityEngine;

namespace Gameplay.Interactions.GameplayMenus.UIPanels
{
    public class StartUIPanel : MonoBehaviour, IInteractable
    {
        private Action _closeCallback;

        public void OnOkButton()
        {
            _closeCallback?.Invoke();
            Destroy(gameObject);
        }
        
        public void Interact(Action closeCallback)
        {
            gameObject.SetActive(true);
            _closeCallback = closeCallback;
        }
    }
}