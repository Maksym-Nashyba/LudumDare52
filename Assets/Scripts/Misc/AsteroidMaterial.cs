using TMPro;
using UnityEngine;

namespace Misc
{
    [CreateAssetMenu(fileName = "NewAsteroidMaterial", menuName = "ScriptableObjects/AsteroidMaterial")]
    public class AsteroidMaterial : ScriptableObject
    {
        public bool Spawns => _spawns;
        [SerializeField] private bool _spawns;
        
        public string Name => _name;
        [SerializeField] private string _name;

        public MaterialType Type => _type;
        [SerializeField] private MaterialType _type;

        public int Price => _price;
        [SerializeField] private int _price;

        public Rarity Rarity => _rarity;
        [SerializeField] private Rarity _rarity;

        public Sprite Sprite => _sprite;
        [SerializeField] private Sprite _sprite;
        
        public Material RenderMaterial => _renderMaterial;
        [SerializeField] private Material _renderMaterial;

        public AsteroidMaterial BiggerChunk => _biggerChunk;
        [SerializeField] private AsteroidMaterial _biggerChunk;
        
        public AsteroidMaterial LargeChunk => _largeChunk;
        [SerializeField] private AsteroidMaterial _largeChunk;
    }
}