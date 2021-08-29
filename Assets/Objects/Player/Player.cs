using System;
using Dialogs;
using EventChannels;
using Objects.Bots.Scripts;
using UnityEngine;

namespace Objects.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private DialogEventChannelSO _dialogEventChannel = default;
        private PlayerMovement _playerMovement;
        public bool BotInteractable => _canInteractWithBots;
        public Bot CurrentFollower => _currentFollower;
        public void SetBotInteractableStatus(bool canInteract)
        {
            _canInteractWithBots = canInteract;
        }

        public void SetCurrentFollower(Bot bot)
        {
            _currentFollower = bot;
        }

        public void ResetCurrentFollower()
        {
            _currentFollower.StopFollowing();
            _currentFollower = null;
        }
  
        public bool IsChangingArea
        {
            get => _isChangningArea;
            set
            {
                if (value)
                {
                    _playerMovement.DisableMovement(false);
                }
                else
                {
                    _playerMovement.EnableMovement();
                }

                _isChangningArea = value;
            }
        }

        private void Awake() {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void OnEnable() {
            _dialogEventChannel.OnDialogOpened += OnDialogOpened;
            _dialogEventChannel.OnDialogClosed += OnDialogClosed;
        }

        private void OnDisable()
        {
            _dialogEventChannel.OnDialogOpened -= OnDialogOpened;
            _dialogEventChannel.OnDialogClosed -= OnDialogClosed;
        }

        private void OnDialogOpened(DialogSO dialog)
        {
            _playerMovement.DisableMovement();
        }

        private void OnDialogClosed()
        {
            _playerMovement.EnableMovement();
        }

        public void AdditionalMove(Vector2 additionalMove)
        {
            _playerMovement.MoveAt((Vector2)transform.position+additionalMove);
        }

        private bool _isChangningArea;
        private bool _canInteractWithBots = true;
        private Bot _currentFollower;
    }
}