using Objects.Bots.Scripts;
using UnityEngine;
using СlothesConfigs.ScriptableObjectConfig;

namespace Traits
{
    [CreateAssetMenu(fileName = "New Useless Trait", menuName = "Traits/Useless", order = 0)]
    public class UselessTrait : ScriptableObject, ITrait
    {
        public string text { get; set; }
        public Bot Target { get; set; }
        public Bot Sender { get; set; }

        public bool IsTraitOfMainTarget { get; set; }

        public string GetTraitText()
        {
            return text;
        }
    }
}