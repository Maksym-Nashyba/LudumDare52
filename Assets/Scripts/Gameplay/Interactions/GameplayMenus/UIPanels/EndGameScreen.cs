using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay.Interactions.GameplayMenus.UIPanels
{
    public class EndGameScreen : MonoBehaviour, IInteractable
    {
        public void OnRestartButton()
        {
            SceneManager.LoadScene(0);
        }
        
        public void Interact(Action closeCallback)
        {
        }
    }
}