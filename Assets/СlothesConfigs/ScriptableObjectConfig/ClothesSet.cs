using UnityEngine;

namespace СlothesConfigs.ScriptableObjectConfig
{
    [CreateAssetMenu(fileName = "New Clothes Set", menuName = "Clothes/Clothes set", order = 0)]
    public class ClothesSet : ScriptableObject
    {
        public ClothesConfig[] Clothes;
    }
}