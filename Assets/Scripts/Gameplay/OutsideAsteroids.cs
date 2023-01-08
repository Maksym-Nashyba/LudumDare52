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

        private void Start()
        {
            _asteroids = SpawnFirstAsteroids();
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
            asteroidRigidbody.velocity = (asteroidRigidbody.position - PickPointInBounds()).normalized * Random.Range(0f, 2f);
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