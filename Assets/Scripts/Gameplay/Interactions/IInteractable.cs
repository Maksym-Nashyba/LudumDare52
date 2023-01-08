using System;

namespace Gameplay.Interactions
{
    public interface IInteractable
    {
        public void Interact(Action closeCallback);
    }
}