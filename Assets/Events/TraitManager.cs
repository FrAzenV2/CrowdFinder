using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Traits;
using EventChannels;
using Objects.Bots.Scripts;

namespace Managers
{
    public class TraitManager : MonoBehaviour
    {
        [SerializeField] private TraitEventChannelSO _traitEventChannel = default;

        // Start is called before the first frame update
        private void Awake()
        {
            _traitEventChannel.OnTraitRequested += GenerateTrait;
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public void GenerateTrait(Bot bot)
        {
            // Create a new trait based on bot data and assign it
            //bot.AssignTrait()
        }
    }
}