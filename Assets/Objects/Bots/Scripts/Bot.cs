using Bots_Configs.ScriptableObjectConfig;
using EventChannels;
using Traits;
using UnityEngine;

namespace Objects.Bots.Scripts
{
    [RequireComponent(typeof(ClickInteractor))]
    public class Bot : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private CollisionInteractor _playerInteractZone;
        [SerializeField] private TraitEventChannelSO _traitEventChannel = default;

        public BotConfig Config => _config;
        public bool IsTarget;

        private void Awake(){
            _clickInteractor = GetComponent<ClickInteractor>();
            _playerInteractZone.OnZoneEntered += OnPlayerEntered;
            _playerInteractZone.OnZoneExited += OnPlayerExited;
            _clickInteractor.OnClicked += OnClicked;
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

        public void ReleaseClick()
        {
            _waitingForPlayer = false;
        }

        private void OnPlayerInteracted()
        {
            // Open dialog etc.
        }

        private void OnPlayerEntered(GameObject player)
        {
            _nearPlayer = true;
            // If we were waiting for player, stop them
            if (_waitingForPlayer){
                player.GetComponent<PlayerMovement>().Stop();
                OnPlayerInteracted();
            }
        }

        private void OnPlayerExited(GameObject player)
        {
            _nearPlayer = false;
        }

        private void OnClicked()
        {
            _waitingForPlayer = true;
        }


        
        private BotConfig _config;
        private ITrait _trait;
        private ClickInteractor _clickInteractor;
        private bool _waitingForPlayer;
        private bool _nearPlayer;
    }
}