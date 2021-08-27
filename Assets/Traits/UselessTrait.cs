using Objects.Bots.Scripts;
using UnityEngine;
using СlothesConfigs.ScriptableObjectConfig;

namespace Traits
{
    [CreateAssetMenu(fileName = "New Useless Trait", menuName = "Traits/Useless", order = 0)]
    public class UselessTrait : ScriptableObject, ITrait
    {
        public string text;
        public Bot Target { get; set; }
        public Bot Sender { get; set; }

        public bool IsTraitOfMainTarget { get; set; }

        public bool Equals(ITrait other)
        {
            return false;
        }

        public string GetTraitText()
        {
            return text;
        }
    }
}