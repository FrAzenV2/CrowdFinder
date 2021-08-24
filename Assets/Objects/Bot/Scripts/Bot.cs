using Bots_Configs.ScriptableObjectConfig;
using UnityEngine;
using Traits;
using EventChannels;

namespace Objects.Bot.Scripts
{
    [RequireComponent(typeof(ClickInteractor))]
    public class Bot : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private CollisionInteractor _playerInteractZone;
        [SerializeField] private TraitEventChannelSO _traitEventChannel = default;

        public BotConfig Config => _config;

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

        private void OnPlayerEntered(GameObject player){
            _nearPlayer = true;
        }

        private void OnPlayerExited(GameObject player){
            _nearPlayer = false;
        }

        private void OnClicked(){
            print("Clicked!");
        }
        
        private BotConfig _config;
        private ITrait _trait;
        private ClickInteractor _clickInteractor;
        private bool _nearPlayer;
    }
}