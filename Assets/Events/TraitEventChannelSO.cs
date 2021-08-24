using UnityEngine;
using System.Collections;
using Objects.Bots.Scripts;
using UnityEngine.Events;
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