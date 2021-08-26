using System;
using Bots_Configs.ScriptableObjectConfig;
using EventChannels;
using Traits;
using Dialogs;
using Objects.LevelControllers;
using UnityEngine;
using СlothesConfigs.ScriptableObjectConfig;


namespace Objects.Bots.Scripts
{
    [RequireComponent(typeof(ClickInteractor))]
    public class Bot : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private BotClothes _botClothes;
        
        [SerializeField] private CollisionInteractor _playerInteractZone;
        [SerializeField] private TraitEventChannelSO _traitEventChannel = default;
        [SerializeField] private DialogEventChannelSO _dialogEventChannel = default;
        public Transform dialogPoint;
        [SerializeField] private ClickInteractor _clickInteractor;
        public BotConfig Config => _config;
        
        public bool IsTarget;
        public POI CurrentPOI;
        
        private void Awake(){
            _playerInteractZone.OnZoneEntered += OnPlayerEntered;
            _playerInteractZone.OnZoneExited += OnPlayerExited;
            _clickInteractor.OnClicked += OnClicked;
            _clickInteractor.OnReleased += OnReleased;
        }

        public void Initialize(BotConfig config)
        {
            _config = config;
            _renderer.sprite = _config.BodySprite;
            foreach (var cloth in config.Clothes)    
            {
                if(cloth==null) continue;
                
                switch (cloth.ClothType)
                {
                    case ClothType.Hat:
                        _botClothes.SetupHat(cloth.ClothSprite, cloth.ClothColor);
                        break;
                    case ClothType.Shirt:
                        _botClothes.SetupShirt(cloth.ClothSprite, cloth.ClothColor);
                        break;
                    case ClothType.Pants:
                        _botClothes.SetupPants(cloth.ClothSprite, cloth.ClothColor);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void SetCurrentPoi(POI poi)
        {
            CurrentPOI = poi;
        }

        public void AssignTrait(ITrait trait)
        {
            _trait = trait;
        }

        private void OnPlayerInteracted()
        {
                //print("Found target!");
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
            
            // TODO: Use events
            if (IsTarget)
            {
                GetComponentInChildren<SpriteRenderer>().color = Color.green;
                dialog.text = "Hey! You found me!";
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

        private bool _waitingForPlayer;

        private bool _nearPlayer;

        private bool _inDialog;
    }
}