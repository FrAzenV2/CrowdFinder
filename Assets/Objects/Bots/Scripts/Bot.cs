using System;
using Bots_Configs.ScriptableObjectConfig;
using EventChannels;
using Traits;
using Dialogs;
using Objects.LevelControllers;
using UnityEngine;
using Managers;
using СlothesConfigs.ScriptableObjectConfig;

namespace Objects.Bots.Scripts
{
    [RequireComponent(typeof(PointAndClickInteractor), typeof(BotMovement))]
    public class Bot : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private BotClothes _botClothes;
        public Transform dialogPoint;
        public BotConfig Config => _config;
        public bool IsTarget;
        public bool IsFakeTarget;
        [HideInInspector] public POI CurrentPOI;

        [Header("Events")]
        [SerializeField] private TraitEventChannelSO _traitEventChannel = default;
        [SerializeField] private DialogEventChannelSO _dialogEventChannel = default;
        
        private void Awake(){
            _botMovement = GetComponent<BotMovement>();
            _playerInteractor = GetComponent<PointAndClickInteractor>();
            _playerInteractor.OnStartedInteraction += OnStartedInteraction;
            _playerInteractor.OnEndedInteraction += OnEndedInteraction;
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

        private void OnStartedInteraction()
        {
            DialogSO dialog;
            if (IsFakeTarget){
                dialog = Config.FakeTargetDialog;
                dialog.fromBot = this;
            }else if (IsTarget) {
                dialog = Config.CorrectTargetDialog;
                dialog.fromBot = this;
            } else {
                if (_trait == null)
                    _traitEventChannel.RequestTrait(this);
                else
                    _traitEventChannel.GenerateTrait(_trait);
                dialog = DialogManager.GetDialogFromTrait(_trait);
            }

            _dialogEventChannel.OpenDialog(dialog);
            _inDialog = true;
            _botMovement.DisableMovement();
        }

        private void OnEndedInteraction()
        {
            _inDialog = false;
            _dialogEventChannel.CloseDialog();
            _botMovement.EnableMovement();
        }

        // private void OnPlayerEntered(GameObject player)
        // {
        //     _nearPlayer = true;
        //     // If we were waiting for player, stop them
        //     if (_waitingForPlayer){
        //         OnPlayerInteracted();
        //     }
        // }

        // private void OnPlayerExited(GameObject player)
        // {
        //     _nearPlayer = false;
        //     if (_inDialog)
        //         _dialogEventChannel.CloseDialog();
        // }

        // private void OnClicked()
        // {
        //     if (_nearPlayer){
        //         OnPlayerInteracted();
        //     } else {
        //         _waitingForPlayer = true;
        //     }
        // }

        // private void OnReleased()
        // {
        //     _waitingForPlayer = false;
        //     if (_inDialog)
        //         _dialogEventChannel.CloseDialog();
        // }

        // private void OnDialogClosed()
        // {
        //     _inDialog = false;
        //     _dialogEventChannel.OnDialogClosed -= OnDialogClosed;
        //     _botMovement.EnableMovement();
        // }

        private BotMovement _botMovement;
        private PointAndClickInteractor _playerInteractor;
        private BotConfig _config;
        private bool _inDialog;
        private ITrait _trait;
    }
}