using Bots_Configs.ScriptableObjectConfig;
using EventChannels;
using Traits;
using Dialogs;
using UnityEngine;


namespace Objects.Bots.Scripts
{
    [RequireComponent(typeof(ClickInteractor))]
    public class Bot : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private CollisionInteractor _playerInteractZone;
        [SerializeField] private TraitEventChannelSO _traitEventChannel = default;
        [SerializeField] private DialogEventChannelSO _dialogEventChannel = default;

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
            // Request a trait if we don't have one
            if (_trait == null)
                _traitEventChannel.RequestTrait(this);
            
            DialogSO dialog = ScriptableObject.CreateInstance<DialogSO>();
            if (_trait != null)
                dialog.text = _trait.GetTraitText();
            else
                dialog.text = "No trait :(";

            _dialogEventChannel.OpenDialog(dialog);
        }

        private void OnPlayerEntered(GameObject player)
        {
            _nearPlayer = true;
            // If we were waiting for player, stop them
            if (_waitingForPlayer){
                _player = player.GetComponent<PlayerMovement>();
                _player.Stop();
                OnPlayerInteracted();
            }
        }

        private void OnPlayerExited(GameObject player)
        {
            _nearPlayer = false;
            _player = null;
        }

        private void OnClicked()
        {
            _waitingForPlayer = true;
            //if (_nearPlayer){
            //    _player.Stop();
            //}
        }

        private BotConfig _config;
        private ITrait _trait;
        private ClickInteractor _clickInteractor;
        private bool _waitingForPlayer;
        private PlayerMovement _player;
        private bool _nearPlayer;
    }
}