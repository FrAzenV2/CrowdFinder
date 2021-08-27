using Dialogs;
using EventChannels;
using UnityEngine;

namespace Objects.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private DialogEventChannelSO _dialogEventChannel = default;
        private PlayerMovement _playerMovement;
        public bool BotInteractable => _canInteractWithBots;
        public void SetBotInteractableStatus(bool canInteract)
        {
            _canInteractWithBots = canInteract;
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
            _dialogEventChannel.OnDialogOpened += OnDialogOpened;
            _dialogEventChannel.OnDialogClosed += OnDialogClosed;
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
    }
}