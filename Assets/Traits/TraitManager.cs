using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Bots.Scripts;
using Objects.LevelControllers.Scripts;
using Random = UnityEngine.Random;
using Traits;
using Dialogs;
using EventChannels;

namespace Managers
{
    public class TraitManager : MonoBehaviour
    {
        [Header("Traits Generation Stats")] [SerializeField] [Range(0, 1)]
        private float _chanceOfGivingTrait = 0.05f;

        [SerializeField] private int _maxTryGetTraitsAttempts = 20;
        [SerializeField] [Range(0, 1)] private float _chanceOfCorrectTargetTrait = 0.5f;
        [SerializeField] [Range(0, 1)] private float _chanceOfClothTraitGeneration = 0.6f;
        [SerializeField] [Range(0, 1)] private float _chanceOfDirectionTraitGeneration = 0.25f;
        [SerializeField] [Range(0, 1)] private float _chanceOfPOITraitGeneration = 0.15f;


        [Header("Runtime Bots data access")] [SerializeField]
        private LevelBotSpawner _botSpawner;

        [SerializeField] private PoiList _poiList;
        
        [Header("Traits")]
        [SerializeField] private List<UselessTrait> _uselessTraits;

        [Header("Events")]
        [SerializeField] private TraitEventChannelSO _traitEventChannel = default;


        // Start is called before the first frame update

        private void Awake()
        {
            _traitEventChannel.OnTraitRequested += GenerateTrait;
            _traits = new List<ITrait>();
        }

        private void GenerateTrait(Bot bot)
        {
            if (bot.IsTarget)
                return;
            //If no trait is given
            if (Random.value > _chanceOfGivingTrait)
            {
                // TODO: Separate trait generation & trait assignment
                ITrait newTrait = _uselessTraits[Random.Range(0, _uselessTraits.Count - 1)];
                newTrait.Sender = bot;
                bot.AssignTrait(newTrait);
                return;
            }

            Bot traitTarget;
            ITrait trait;
            var attempts = 0;
            do
            {
                attempts++;
                var isTraitAboutTarget = false;
                if (Random.value <= _chanceOfCorrectTargetTrait)
                {
                    traitTarget = _botSpawner.TargetBot;
                    isTraitAboutTarget = true;
                }
                else
                {
                    traitTarget = _botSpawner.FakeTargets[Random.Range(0, _botSpawner.FakeTargets.Count - 1)];
                }


                if (Random.value <= _chanceOfPOITraitGeneration)
                {
                    trait = ScriptableObject.CreateInstance<POITrait>();
                    ((POITrait) trait).TargetPoi = traitTarget.CurrentPOI;
                    
                    if (traitTarget.CurrentPOI == null)
                        throw new ArgumentNullException();
                }
                else if (Random.value <= _chanceOfDirectionTraitGeneration)
                {
                    trait = ScriptableObject.CreateInstance<DirectionTrait>();
                    ((DirectionTrait) trait).CalculateDirection(traitTarget, bot);
                }
                else
                {
                    trait = ScriptableObject.CreateInstance<ClothTrait>();
                    var cloth = traitTarget.Config.Clothes[Random.Range(0, traitTarget.Config.Clothes.Length - 1)];
                    if (cloth == null)
                    {
                        trait = _uselessTraits[Random.Range(0, _uselessTraits.Count - 1)];
                        trait.Sender = bot;
                        continue;
                    }

                    ((ClothTrait) trait).Cloth = cloth;
                }

                trait.Sender = bot;
                trait.Target = traitTarget;
                trait.IsTraitOfMainTarget = isTraitAboutTarget;

                if (!_traits.Contains(trait))
                {
                    _traits.Add(trait);
                    break;
                }
            } while (trait == null && attempts < _maxTryGetTraitsAttempts);

            if (attempts >= _maxTryGetTraitsAttempts)
            {
                ITrait newTrait = _uselessTraits[Random.Range(0, _uselessTraits.Count - 1)];
                newTrait.Sender = bot;
                bot.AssignTrait(newTrait);
                return;
            }
            
            //
            bot.AssignTrait(trait);
            _traitEventChannel.GenerateTrait(trait);
        }

        private List<ITrait> _traits;
    }
}