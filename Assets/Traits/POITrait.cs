using Objects.Bot.Scripts;
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
        
        public string GetTraitText()
        {
            var trait = Target.Config.BotName + " around " + TargetPoi.POIName;
            return trait;
        }
    }
}