using Objects.Bots.Scripts;
using Objects.LevelControllers;
using UnityEngine;

namespace Traits
{
    [CreateAssetMenu(fileName = "New POI Trait", menuName = "Traits/POI trait", order = 0)]
    public class POITrait : ScriptableObject, ITrait
    {
        public POI TargetPoi;
        public Bot Target { get; set; }
        public Bot Sender { get; set; }

        public bool IsTraitOfMainTarget { get; set; }

        public string GetTraitText()
        {
            var trait = $"<color=\"red\">{Target.Config.BotName}</color> is around {TargetPoi.POIName}";
            return trait;
        }

        public bool Equals(ITrait other)
        {
            if (other.GetType() != GetType())
                return false;
            if (other.Target == Target && ((POITrait) other).TargetPoi.Equals(TargetPoi))
                return true;
            return false;
        }
    }
}