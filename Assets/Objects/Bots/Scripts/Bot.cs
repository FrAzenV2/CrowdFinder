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

        [SerializeField] private BotConfig _staticBotConfig;
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

        private void Start()
        {
            if(_staticBotConfig != null)
                Initialize(_staticBotConfig);
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

        public void StopFollowing()
        {
            _botMovement.StopFollowing();
        }

        private void OnStartedInteraction()
        {
            if(_config.IsBotStatic) return;
            DialogSO dialog;
            if (IsFakeTarget || IsTarget){
                dialog = Config.TargetFoundDialog;
                dialog.fromBot = this;
                SetPlayerFollower();
                _playerInteractor.TryBlockPlayerInteractions();
            } else {
                if (_trait == null)
                    _traitEventChannel.RequestTrait(this);
                else if (!(_trait is UselessTrait))
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

        private void SetPlayerFollower()
        {
            if(!_playerInteractor.InteractingObject.TryGetComponent(out Player.Player player)) return;
            _botMovement.StartFollowing(_playerInteractor.InteractingObject.transform);
            player.SetCurrentFollower(this);
        }

        private void OnDrawGizmos() {
            if (_inDialog){
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, 1.0f);
            }
        }

        private BotMovement _botMovement;
        private PointAndClickInteractor _playerInteractor;
        private BotConfig _config;
        private bool _inDialog;
        private ITrait _trait;
    }
}