using System;
using System.Collections.Generic;
using Asteroids;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class OutsideAsteroids : MonoBehaviour
    {
        [SerializeField] private AsteroidFactory _asteroidFactory;
        [SerializeField] private BoxCollider _spawnBox;
        [SerializeField] private int _targetAsteroidCount;
        private List<Asteroid> _asteroids;
        private int _currentIndex;
        
        private void Start()
        {
            _asteroids = SpawnFirstAsteroids();
            _currentIndex = 0;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.velocity *= -1f;
            }
        }

        public Asteroid GetNextAsteroid()
        {
            _currentIndex++;
            if (_currentIndex == _asteroids.Count) _currentIndex = 0;
            return _asteroids[_currentIndex];
        }
        
        public void RemoveAsteroid(Asteroid asteroid)
        {
            if (_asteroids.Contains(asteroid))
            {
                _asteroids.Remove(asteroid);
            }
            while (_asteroids.Count < _targetAsteroidCount)
            {
                _asteroids.Add(SpawnAsteroid());
            }
        }
        
        private List<Asteroid> SpawnFirstAsteroids()
        {
            List<Asteroid> result = new List<Asteroid>();
            for (int i = 0; i < _targetAsteroidCount; i++)
            {
                result.Add(SpawnAsteroid());
            }
            return result;
        }

        private Asteroid SpawnAsteroid()
        {
            Asteroid asteroid = _asteroidFactory.BuildAsteroid();
            Rigidbody asteroidRigidbody = asteroid.GetRigidbody();
            asteroidRigidbody.useGravity = false;
            asteroidRigidbody.position = PickPointInBounds();
            asteroidRigidbody.velocity = (PickPointInBounds() - asteroidRigidbody.position).normalized * Random.Range(0f, 2f);
            return asteroid;
        }

        private Vector3 PickPointInBounds()
        {
            Bounds bounds = _spawnBox.bounds;
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
    }
}