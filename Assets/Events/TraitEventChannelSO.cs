using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Objects.Bot.Scripts;
using Traits;

namespace EventChannels
{
    public class TraitEventChannelSO : ScriptableObject
    {
        public UnityAction<Bot> OnTraitRequested;
        public UnityAction<ITrait> OnTraitGenerated;
        
        public void RequestTrait(Bot bot)
        {
            OnTraitRequested.Invoke(bot);
        }

        public void GenerateTrait(ITrait trait)
        {
            OnTraitGenerated.Invoke(trait);
        }
    }
}
