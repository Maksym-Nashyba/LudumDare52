﻿using System;
using Asteroids;
using UnityEngine;

namespace Gameplay.Interactions
{
    public class AsteroidCatcher : MonoBehaviour, IInteractable
    {
        [SerializeField] private OutsideAsteroids _outsideAsteroids;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private AsteroidCatcherDisplay _display;
        private Action _closeCallback;
        private Transform _cameraTarget;
        
        private void Update()
        {
            if (_closeCallback == null) return;
            if (Input.GetKeyDown(KeyCode.Escape)) Close();
            if (_cameraTarget != null)
            {
                _cameraTransform.LookAt(_cameraTarget);
            }
        }
        
        public void Interact(Action closeCallback)
        {
            _closeCallback = closeCallback;
            _display.Show(OnNextButton, OnCatchButton);
            LockOnAsteroid(_outsideAsteroids.GetNextAsteroid());
        }

        private void LockOnAsteroid(Asteroid asteroid)
        {
            _cameraTarget = asteroid.transform;
            _display.DisplayAsteroid(asteroid);
        }

        private void OnNextButton()
        {
            LockOnAsteroid(_outsideAsteroids.GetNextAsteroid());
        }

        private void OnCatchButton()
        {
            Close();
        }
        
        private void Close()
        {
            _cameraTarget = null;
            _display.Hide();
            _closeCallback.Invoke();
            _closeCallback = null;
        }
    }
}