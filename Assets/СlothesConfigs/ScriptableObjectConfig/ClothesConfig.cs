using System;
using UnityEngine;

namespace СlothesConfigs.ScriptableObjectConfig
{
    [CreateAssetMenu(fileName = "New Clothes Config", menuName = "Clothes/Clothes Config", order = 0)]
    public class ClothesConfig : ScriptableObject, IEquatable<ClothesConfig>
    {
        [SerializeField] private ClothType _clothType;
        [SerializeField] private Sprite _clothSprite;
        [SerializeField] private Color _clothColor;
        [SerializeField] private string _clothName;
        
        public ClothType ClothType => _clothType;
        public Sprite ClothSprite => _clothSprite;
        public Color ClothColor => _clothColor;
        public string ClothName => _clothName;

        public bool Equals(ClothesConfig other)
        {
            return (_clothType == other.ClothType && _clothSprite == other.ClothSprite && _clothColor == other.ClothColor && _clothName == other.ClothName);
        }
    }

    public enum ClothType
    {
        Hat,
        Shirt,
        Pants
    }
}