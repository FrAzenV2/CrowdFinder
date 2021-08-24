﻿using Bots_Configs.ScriptableObjectConfig;
using EventChannels;
using Traits;
using UnityEngine;

namespace Objects.Bots.Scripts
{
    public class Bot : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private InteractZone _playerInteractZone;
        [SerializeField] private TraitEventChannelSO _traitEventChannel = default;

        public BotConfig Config => _config;
        public bool IsTarget;

        private void Awake()
        {
            _playerInteractZone.OnZoneEntered += OnPlayerEntered;
            _playerInteractZone.OnZoneExited += OnPlayerExited;
        }

        public void Initialize(BotConfig config)
        {
            _config = config;
            _renderer.sprite = _config.BodySprite;
        }

        public void AssignTrait(ITrait trait)
        {
            _trait = trait;
        }

        private void OnPlayerEntered(GameObject player)
        {
            _nearPlayer = true;
        }

        private void OnPlayerExited(GameObject player)
        {
            _nearPlayer = false;
        }

        private BotConfig _config;
        private ITrait _trait;
        private bool _nearPlayer;
    }
}