using Objects.Bots.Scripts;
using UnityEngine;
using System;
using СlothesConfigs.ScriptableObjectConfig;

namespace Traits
{
    [CreateAssetMenu(fileName = "New Cloth Trait", menuName = "Traits/Cloth", order = 0)]
    public class ClothTrait : ScriptableObject, ITrait
    {
        public ClothesConfig Cloth;

        public Bot Target { get; set; }
        public Bot Sender { get; set; }

        public bool IsTraitOfMainTarget { get; set; }

        public bool Equals(ITrait other)
        {
            if (other.GetType() != GetType())
                return false;
            if (other.Target == Target && ((ClothTrait) other).Cloth.Equals(Cloth))
                return true;
            return false;
        }

        public string GetTraitText()
        {
            var trait = $"<color=\"red\">{Target.Config.BotName}</color> is wearing {Cloth.ClothName}";
            return trait;
        }
    }
}