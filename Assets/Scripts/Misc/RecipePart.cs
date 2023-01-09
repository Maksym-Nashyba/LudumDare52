using System;
using UnityEngine;

namespace Misc
{
    [Serializable]
    public class RecipePart
    {
        public AsteroidMaterial Material => _material;
        [SerializeField] private AsteroidMaterial _material;
        
        public int Amount => _amount;
        [SerializeField] private int _amount;
    }
}