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
        [Header("Traits Generation Stats")]
        [SerializeField]
        [Range(0, 1)]
        private float _chanceOfGivingTrait = 0.05f;

        [SerializeField] private int _maxTryGetTraitsAttempts = 20;
        [SerializeField] [Range(0, 1)] private float _chanceOfCorrectTargetTrait = 0.5f;
        [SerializeField] [Range(0, 1)] private float _chanceOfClothTraitGeneration = 0.6f;
        [SerializeField] [Range(0, 1)] private float _chanceOfDirectionTraitGeneration = 0.25f;
        [SerializeField] [Range(0, 1)] private float _chanceOfPOITraitGeneration = 0.15f;
        [SerializeField] [Range(0, 1)] private float _chanceOfFriendTraitGeneration = 0.1f;


        [Header("Runtime Bots data access")]
        [SerializeField]
        private LevelBotSpawner _botSpawner;

        [SerializeField] private PoiList _poiList;

        [Header("Traits")]
        [SerializeField] private List<String> _uselessTraitStrings;

        [Header("Events")]
        [SerializeField] private TraitEventChannelSO _traitEventChannel = default;
        
        private void Awake()
        {
            _traits = new List<ITrait>();
        }

        private void OnEnable()
        {
            _traitEventChannel.OnTraitRequested += GenerateTrait;
            
        }

        private void OnDisable()
        {
            _traitEventChannel.OnTraitRequested += GenerateTrait;
        }

        private void GenerateTrait(Bot bot)
        {
            if (bot.IsTarget)
                return;
            //If no trait is given
            if (Random.value > _chanceOfGivingTrait)
            {
                // TODO: Separate trait generation & trait assignment
                bot.AssignTrait(GenerateUselessTrait(bot));
                return;
            }

            ITrait trait;

            if (Random.value <= _chanceOfFriendTraitGeneration)
            {
                trait = GenerateFriendTrait(bot);
            }
            else
            {
                trait = GenerateRandomTraitForTarget(bot);
            }

            if (!_traits.Contains(trait))
            {
                _traits.Add(trait);
            }
            else
            {
                bot.AssignTrait(GenerateUselessTrait(bot));
                return;
            }

            bot.AssignTrait(trait);
            _traitEventChannel.GenerateTrait(trait);
        }

        private ITrait GenerateRandomTraitForTarget(Bot traitSource, Bot traitTarget = null)
        {
            ITrait trait;
            int attempt = 0;
            var isTraitAboutTarget = false;
            bool randomTarget = traitTarget == null;

            while (attempt < _maxTryGetTraitsAttempts)
            {
                attempt++;
                if (randomTarget)
                {
                    if (Random.value <= _chanceOfCorrectTargetTrait)
                    {
                        traitTarget = _botSpawner.TargetBot;
                        isTraitAboutTarget = true;
                    }
                    else
                    {
                        traitTarget = _botSpawner.FakeTargets[Random.Range(0, _botSpawner.FakeTargets.Count - 1)];
                        isTraitAboutTarget = false;
                    }
                }
                if (Random.value <= _chanceOfPOITraitGeneration)
                {
                    trait = ScriptableObject.CreateInstance<POITrait>();
                    ((POITrait)trait).TargetPoi = traitTarget.CurrentPOI;

                    if (traitTarget.CurrentPOI == null)
                        throw new ArgumentNullException();
                }
                else if (Random.value <= _chanceOfDirectionTraitGeneration)
                {
                    trait = ScriptableObject.CreateInstance<DirectionTrait>();
                    ((DirectionTrait)trait).CalculateDirection(traitTarget, traitSource);
                }
                else
                {
                    trait = ScriptableObject.CreateInstance<ClothTrait>();
                    var cloth = traitTarget.Config.Clothes[Random.Range(0, traitTarget.Config.Clothes.Length - 1)];
                    ((ClothTrait)trait).Cloth = cloth;
                }
                ConfigureTrait(trait, traitSource, traitTarget);
                trait.IsTraitOfMainTarget = isTraitAboutTarget;
                if (!_traits.Contains(trait))
                    return trait;
                else if (attempt == _maxTryGetTraitsAttempts - 1)
                    return trait;
            }
            return null;
        }

        private ITrait GenerateUselessTrait(Bot traitSource)
        {
            UselessTrait trait = ScriptableObject.CreateInstance<UselessTrait>();
            trait.text = _uselessTraitStrings[Random.Range(0, _uselessTraitStrings.Count - 1)];
            trait.Sender = traitSource;
            return trait;
        }

        private ITrait GenerateFriendTrait(Bot traitSource)
        {
            ITrait trait;

            Bot traitTarget;
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

            int attempt = 0;
            Bot friend = _botSpawner.Bots[Random.Range(0, _botSpawner.Bots.Count - 1)];
            while (attempt < 20)
            {
                if (friend != traitSource)
                    break;

                friend = _botSpawner.Bots[Random.Range(0, _botSpawner.Bots.Count - 1)];
                attempt++;
            }

            // Generate extra traits
            ITrait trait1 = GenerateRandomTraitForTarget(traitSource, friend);
            _traits.Add(trait1);
            ITrait trait2 = GenerateRandomTraitForTarget(traitSource, friend);
            _traits.Add(trait2);

            ConfigureTrait(trait1, traitSource, friend);
            ConfigureTrait(trait2, traitSource, friend);

            // Generate a trait for the friend
            ITrait friendsTrait = GenerateRandomTraitForTarget(friend, traitTarget);
            friendsTrait.IsTraitOfMainTarget = isTraitAboutTarget;
            friend.AssignTrait(friendsTrait);

            // Create friend trait
            trait = ScriptableObject.CreateInstance<FriendTrait>();
            ((FriendTrait)trait).trait1 = trait1;
            ((FriendTrait)trait).trait2 = trait2;
            ConfigureTrait(trait, traitSource, friend);
            return trait;
        }

        private void ConfigureTrait(ITrait trait, Bot sender, Bot target)
        {
            trait.Sender = sender;
            trait.Target = target;
        }


        private List<ITrait> _traits;
    }
}