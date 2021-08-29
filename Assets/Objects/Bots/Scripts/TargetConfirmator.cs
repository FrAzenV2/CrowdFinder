using System;
using Dialogs;
using EventChannels;
using UnityEngine;

namespace Objects.Bots.Scripts
{
    public class TargetConfirmator : MonoBehaviour
    {

        [SerializeField] private Bot _thisBot;
        
        [Header("Events")]
        [SerializeField] private DialogEventChannelSO _dialogEventChannel = default;
        [SerializeField] private GameEventChannelSO _gameEventChannel = default;

        [Header("Dialogs")]
        [SerializeField] private string _correctTargetFoundDialog = "Yeah, thanks for finding him!";
        [SerializeField] private string _fakeTargetFoundDialog = "Idk who is that...";
        [SerializeField] private string _noTargetFoundDialog = "Hope you will find him soon";
        private void OnEnable()
        {
            _playerInteractor = GetComponent<PointAndClickInteractor>();
            _playerInteractor.OnStartedInteraction += OnStartedInteraction;
            _playerInteractor.OnEndedInteraction += OnEndedInteraction;
        }

        private void OnDisable()
        {
            _playerInteractor.OnStartedInteraction -= OnStartedInteraction;
            _playerInteractor.OnEndedInteraction -= OnEndedInteraction;
        }

        public event Action CorrectTargetFound;
        public event Action FakeTargetFound;
        
        private void OnStartedInteraction()
        {
            if(!_playerInteractor.InteractingObject.TryGetComponent(out Player.Player player)) return;
            
            var dialog = ScriptableObject.CreateInstance<DialogSO>();
            dialog.fromBot = _thisBot;
            if (player.CurrentFollower != null)
            {
                if (player.CurrentFollower.IsTarget)
                {
                    dialog.text = _correctTargetFoundDialog;
                    CorrectTargetFound?.Invoke();
                    _gameEventChannel.Win();
                }

                if (player.CurrentFollower.IsFakeTarget)
                {
                    dialog.text = _fakeTargetFoundDialog;
                    FakeTargetFound?.Invoke();
                }

                player.SetBotInteractableStatus(true);
                player.CurrentFollower.StopFollowing();
                player.ResetCurrentFollower();
            }
            else
            {
                dialog.text = _noTargetFoundDialog;
            }

            _dialogEventChannel.OpenDialog(dialog);
        }

        private void OnEndedInteraction()
        {
            
        }

        private PointAndClickInteractor _playerInteractor;
    }
}