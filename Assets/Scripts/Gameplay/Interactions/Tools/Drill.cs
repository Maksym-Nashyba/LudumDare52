using UnityEngine;

namespace Gameplay.Interactions.Tools
{
    public class Drill : Tool
    {
        public float Radius => _radius;
        [SerializeField] private float _radius;
        public float Strength => _strength;
        [SerializeField] private float _strength;
    }
}