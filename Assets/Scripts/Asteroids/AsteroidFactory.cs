using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection;
using Asteroids.Meshes;
using Misc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class AsteroidFactory : MonoBehaviour
    {
        private AsteroidMaterial[] _materials;
        [SerializeField] private GameObject _asteroidPrefab;
        [SerializeField] private GameObject _asteroidLayerPrefab;

        private void Awake()
        {
            _materials = LoadMaterials();
        }

        private void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                Asteroid asteroid = BuildAsteroid();
                asteroid.transform.position = Vector3.up * i;
            }
        }

        public Asteroid BuildAsteroid()
        {
            Asteroid asteroid = Instantiate(_asteroidPrefab).GetComponent<Asteroid>();

            Size size = PickSize();
            SetField(asteroid, "_size", size);
            Queue<AsteroidLayer> layers = BuildLayers(size, asteroid.transform);
            SetField(asteroid, "_asteroidLayers", layers);

            asteroid.GetComponent<AsteroidMesh>().BuildMeshes(new Queue<AsteroidLayer>(layers));
            return asteroid;
        }

        private Queue<AsteroidLayer> BuildLayers(Size size, Transform asteroidTransform)
        {
            int layerCount = (int) size;
            Queue<AsteroidLayer> result = new Queue<AsteroidLayer>();
            for (int i = 0; i < layerCount; i++)
            {
                AsteroidMaterial material = PickMaterial();
                AsteroidLayer layer = Instantiate(_asteroidLayerPrefab, asteroidTransform).GetComponent<AsteroidLayer>();
                layer.SetUp(material, 1f-EaseFunctions.EaseInCirc(Random.Range(0f, 1f)));
                result.Enqueue(layer);
            }
            return result;
        }
        
        

        private void SetField(Asteroid asteroid, string fieldName, object value)
        {
            FieldInfo field = asteroid.GetType()
                .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(asteroid, value);
        }
        
        private Size PickSize()
        {
            float random = Random.Range(1f, 4f);
            return (Size)(int)random;
        }

        private AsteroidMaterial PickMaterial()
        {
            float random01 = Random.Range(0f, 1f);
            random01 = EaseFunctions.EaseInCirc(random01);
            Rarity rarity = (Rarity)random01.Remap(0f, 1f, 0f, (float)Rarity.Max + 0.99f);
            AsteroidMaterial[] materialsWithRarity = _materials.Where(material => material.Rarity == rarity).ToArray();
            return materialsWithRarity[Random.Range(0, materialsWithRarity.Length)];
        }
        
        private AsteroidMaterial[] LoadMaterials()
        {
            return Resources.LoadAll<AsteroidMaterial>("Materials");
        }
    }
}