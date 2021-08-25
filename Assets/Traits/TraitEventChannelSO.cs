using UnityEngine;
using System.Collections;
using Objects.Bots.Scripts;
using UnityEngine.Events;
using Traits;

namespace EventChannels
{
    [CreateAssetMenu(fileName = "newTraitEventChannel", menuName = "Events/Trait Event Channel")]
    public class TraitEventChannelSO : ScriptableObject
    {
        public UnityAction<Bot> OnTraitRequested = default;
        public UnityAction<ITrait> OnTraitGenerated = default;

        public void RequestTrait(Bot bot)
        {
            if (OnTraitRequested != null)
                OnTraitRequested.Invoke(bot);
        }

        public void GenerateTrait(ITrait trait)
        {
            if (OnTraitGenerated != null)
                OnTraitGenerated.Invoke(trait);
        }
    }
}