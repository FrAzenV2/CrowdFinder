using UnityEngine;

namespace СlothesConfigs.ScriptableObjectConfig
{
    [CreateAssetMenu(fileName = "New Clothes Config", menuName = "Clothes/Clothes Config", order = 0)]
    public class ClothesConfig : ScriptableObject
    {
        [SerializeField] private ClothType _clothType;
        [SerializeField] private Sprite _clothSprite;
        [SerializeField] private string _clothName;
            
        public ClothType ClothType => _clothType;
        public Sprite ClothSprite => _clothSprite;
        public string ClothName => _clothName;
    }

    public enum ClothType
    {
        Hat,
        Shirt,
        Pants
    }
}