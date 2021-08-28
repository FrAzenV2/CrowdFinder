using System;
using System.Collections;
using Dialogs;
using EventChannels;
using Managers;
using Objects.Bots.Scripts;
using UnityEngine;

namespace Objects.LevelControllers.Scripts
{
    public class OnBoardingDialog : MonoBehaviour
    {
        [SerializeField] private ClickManager _clickManager;
        [SerializeField] private Bot _bot;
        [SerializeField] private string[] _dialog;
        [SerializeField] private DialogEventChannelSO _dialogEventChannel;
        [SerializeField] private float _dialogDelay = 2.5f;
        [SerializeField] private ClickInteractor _clickInteractor;

        public void StartDialogDelay(Action callback)
        {
            _callback = callback;
            StartCoroutine(WaitForDelay());
        }

        private void OnEnable()
        {
            _clickInteractor.OnReleased += ContinueDialog;
        }

        private void OnDisable()
        {
            _clickInteractor.OnReleased -= ContinueDialog;
        }

        private void ContinueDialog()
        {
            _dialogStage++;
            if (_dialogStage >= _dialog.Length)
            {
                _dialogEventChannel.CloseDialog();
                _callback.Invoke();
                enabled = false;
                return;
            }
            var dialog = ScriptableObject.CreateInstance<DialogSO>();
            dialog.fromBot = _bot;
            dialog.text = _dialog[_dialogStage];
            _dialogEventChannel.UpdateDialog(dialog);
        }

        private void StartDialog()
        {
            _clickManager.SetLastInteractor(_clickInteractor);
            _dialogStage = 0;
            var dialog = ScriptableObject.CreateInstance<DialogSO>();
            dialog.fromBot = _bot;
            dialog.text = _dialog[_dialogStage];
            _dialogEventChannel.OpenDialog(dialog);
        }

        private IEnumerator WaitForDelay()
        {
            yield return new WaitForSeconds(_dialogDelay);
            StartDialog();
        }

        private int _dialogStage;
        private Action _callback;
    }
}