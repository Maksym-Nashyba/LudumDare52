using System;
using Gameplay.Interactions.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Interactions.GameplayMenus.UIPanels
{
    public class InstrumentSelectionDisplay : MonoBehaviour
    {
        [SerializeField] private Interactor _interactor;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private InstrumentHolder _instruments;
        [SerializeField] private Image[] _icons;
        
        private void Awake()
        {
            _instruments.ToolChanged += OnToolChanged;
            _interactor.Locked += OnInteractorLocked;
            _interactor.Unlocked += OnInteractorUnlocked;
        }

        private void OnDestroy()
        {
            _instruments.ToolChanged -= OnToolChanged;
            _interactor.Locked -= OnInteractorLocked;
            _interactor.Unlocked -= OnInteractorUnlocked;
        }

        private void OnInteractorLocked()
        {
            _canvasGroup.alpha = 0f;
        }
        
        private void OnInteractorUnlocked()
        {
            _canvasGroup.alpha = 1f;
        }

        private void OnToolChanged(Tool tool)
        {
            int index = Array.IndexOf(_instruments.Tools, tool);
            SelectIcon(index);
        }

        private void SelectIcon(int index)
        {
            foreach (Image image in _icons)
            {
                image.color = new Color(1f, 1f, 1f, 0.4f);
            }            
            _icons[index].color = new Color(1f, 1f, 1f, 1f);
        }
    }
}