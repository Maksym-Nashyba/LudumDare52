using System;
using System.Collections.Generic;
using System.Linq;
using Misc;
using UnityEngine;

namespace Inventory
{
    public class Inventory : MonoBehaviour
    {
        public event Action Changed;
        private Dictionary<AsteroidMaterial, int> _materials;
        
        private void Awake()
        {
            _materials = new Dictionary<AsteroidMaterial, int>();
        }

        private void Start()
        {
            AsteroidMaterial[] asteroidMaterials = Resources.LoadAll<AsteroidMaterial>("Materials/");
            Add(asteroidMaterials[1], 5);
            Add(asteroidMaterials[3], 14);
        }

        public AsteroidMaterial[] GetMaterials()
        {
            return  _materials.Keys.ToArray();
        }

        public int GetAmount(AsteroidMaterial material)
        {
            return _materials[material];
        }

        public void Add(AsteroidMaterial material, int amount)
        {
            if (!_materials.ContainsKey(material))
            {
                _materials.Add(material, amount);
                return;
            }
            _materials[material] += amount;
            Changed?.Invoke();
        }

        public void Remove(AsteroidMaterial material, int amount)
        {
            if (!TryRemove(material, amount)) throw new ArgumentOutOfRangeException();
            _materials[material] -= amount;
            if (_materials[material] == 0) _materials.Remove(material);
            Changed?.Invoke();
        }

        private bool TryRemove(AsteroidMaterial material, int amount)
        {
            return _materials[material] >= amount;
        }
    }
}