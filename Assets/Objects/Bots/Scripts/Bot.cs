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

        public Transform dialogPoint;
        public BotConfig Config => _config;
        public bool IsTarget;

        private void Awake(){
            _clickInteractor = GetComponent<ClickInteractor>();
            _playerInteractZone.OnZoneEntered += OnPlayerEntered;
            _playerInteractZone.OnZoneExited += OnPlayerExited;
            _clickInteractor.OnClicked += OnClicked;
            _clickInteractor.OnReleased += OnReleased;
            //_dialogEventChannel.OnDialogClosed += OnDialogClosed;
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

        private void OnPlayerInteracted()
        {
            _waitingForPlayer = false;
            if (_inDialog)
                return;
            // Request a trait if we don't have one
            if (_trait == null)
                _traitEventChannel.RequestTrait(this);
            
            DialogSO dialog = ScriptableObject.CreateInstance<DialogSO>();
            dialog.fromBot = this;
            if (_trait != null)
                dialog.text = _trait.GetTraitText();
            else
                dialog.text = "No trait :(";
            if (IsTarget){
                dialog.text = "You found me!";
                GetComponentInChildren<SpriteRenderer>().color = Color.green;
            }

            _dialogEventChannel.OpenDialog(dialog);
            _dialogEventChannel.OnDialogClosed += OnDialogClosed;
            _inDialog = true;
        }

        private void OnPlayerEntered(GameObject player)
        {
            _nearPlayer = true;
            // If we were waiting for player, stop them
            if (_waitingForPlayer){
                OnPlayerInteracted();
            }
        }

        private void OnPlayerExited(GameObject player)
        {
            _nearPlayer = false;
            if (_inDialog)
                _dialogEventChannel.CloseDialog();
        }

        private void OnClicked()
        {
            if (_nearPlayer){
                OnPlayerInteracted();
            } else {
                _waitingForPlayer = true;
            }
        }

        private void OnReleased()
        {
            _waitingForPlayer = false;
            if (_inDialog)
                _dialogEventChannel.CloseDialog();
        }

        private void OnDialogClosed()
        {
            _inDialog = false;
            _dialogEventChannel.OnDialogClosed -= OnDialogClosed;
        }

        private BotConfig _config;
        private ITrait _trait;
        private ClickInteractor _clickInteractor;
        private bool _waitingForPlayer;
        private bool _nearPlayer;
        private bool _inDialog;
    }
}