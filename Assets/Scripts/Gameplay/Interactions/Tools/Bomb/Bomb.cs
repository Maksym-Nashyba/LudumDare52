using System;
using Asteroids;
using UnityEngine;

namespace Gameplay.Interactions.Tools
{
    public class Bomb : Tool
    {
        [SerializeField] private Interactor _interactor;
        [SerializeField] private GameObject _placedBombPrefab;
        private GameObject _placedBomb;

        private void Update()
        {
            if(Input.GetMouseButtonDown(0) && _placedBomb == null) _interactor.ApplyTool(this); 
        }

        public override void Apply(Asteroid asteroid, Vector3 position)
        {
            _placedBomb = Instantiate(_placedBombPrefab);
            _placedBomb.GetComponent<PlacedBomb>().AttachToAsteroid(asteroid, position);
        }
    }
}