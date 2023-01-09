using System;
using System.Collections.Generic;
using System.Linq;
using Misc;
using UnityEngine;

namespace Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        public event Action Changed;
        public event Action<int> BalanceChanged;
        private int _balance;
        private Dictionary<AsteroidMaterial, int> _materials;
        

        private void Awake()
        {
            _materials = new Dictionary<AsteroidMaterial, int>();
            _balance = 0;
        }

        public int GetBalance() => _balance;

        public void AddMoney(int amount)
        {
            _balance += Mathf.Clamp(amount, 0, int.MaxValue);
            BalanceChanged?.Invoke(_balance);
        }

        public AsteroidMaterial[] GetMaterials()
        {
            return  _materials.Keys.ToArray();
        }

        public int GetAmount(AsteroidMaterial material)
        {
            if (!_materials.ContainsKey(material)) return 0;
            return _materials[material];
        }

        public void Add(AsteroidMaterial material, int amount)
        {
            if (!_materials.ContainsKey(material))
            {
                _materials.Add(material, amount);
                Changed?.Invoke();
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
        
        public bool HasEnoughMaterials(RecipePart[] recipe)
        {
            foreach (RecipePart recipePart in recipe)
            {
                if(recipePart.Amount > GetAmount(recipePart.Material)) return false;
            }
            return true;
        }

        private bool TryRemove(AsteroidMaterial material, int amount)
        {
            return _materials[material] >= amount;
        }
    }
}