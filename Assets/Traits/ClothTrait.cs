using Objects.Bot.Scripts;
using UnityEngine;
using СlothesConfigs.ScriptableObjectConfig;

namespace Traits
{
    [CreateAssetMenu(fileName = "New Cloth Trait", menuName = "Traits/Cloth", order = 0)]
    public class ClothTrait : ScriptableObject, ITrait
    {
        public ClothesConfig Cloth;

        public Bot Target { get; set; }
        public Bot Sender { get; set; }
        
        public string GetTraitText()
        {
            var trait = Target.Config.BotName + " wearing " + Cloth.ClothName;
            return trait;
        }
    }
}