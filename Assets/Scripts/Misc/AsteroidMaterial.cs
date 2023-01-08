using UnityEngine;

namespace Misc
{
    [CreateAssetMenu(fileName = "NewAsteroidMaterial", menuName = "ScriptableObjects/AsteroidMaterial")]
    public class AsteroidMaterial : ScriptableObject
    {
        public string Name => _name;
        [SerializeField] private string _name;

        public MaterialType Type => _type;
        [SerializeField] private MaterialType _type;

        public int Price => _price;
        [SerializeField] private int _price;

        public Rarity Rarity => _rarity;
        [SerializeField] private Rarity _rarity;

        public Material RenderMaterial => _renderMaterial;
        [SerializeField] private Material _renderMaterial;
    }
}