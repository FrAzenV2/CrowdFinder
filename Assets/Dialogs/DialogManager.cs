using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Bots.Scripts;
using EventChannels;
using Dialogs;
using Traits;

namespace Managers
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] private DialogEventChannelSO _dialogEventChannel = default;
        [SerializeField] private DialogBox _dialogBoxPrefab;
        [SerializeField] private Transform _dialogParent;

        private void OnEnable()
        {
            _dialogEventChannel.OnDialogOpened += OpenDialog;
            _dialogEventChannel.OnDialogUpdated += UpdateDialog;
            _dialogEventChannel.OnDialogClosed += CloseDialog;
        }

        private void OnDisable()
        {
            _dialogEventChannel.OnDialogOpened -= OpenDialog;
            _dialogEventChannel.OnDialogUpdated -= UpdateDialog;
            _dialogEventChannel.OnDialogClosed -= CloseDialog;
        }

        public void OpenDialog(DialogSO dialog)
        {
            var canvasPosition = Camera.main.WorldToScreenPoint(dialog.fromBot.dialogPoint.transform.position);
            print(_dialogParent);
            DialogBox dialogBox = Instantiate(_dialogBoxPrefab, canvasPosition , Quaternion.identity, _dialogParent);
            dialogBox.Initialize(dialog, dialog.fromBot.dialogPoint.transform);
            
            _currentDialog = dialogBox;
        }

        public void UpdateDialog(DialogSO dialog)
        {
            if (_currentDialog == null)
                return;
            _currentDialog.UpdateDialog(dialog);
        }

        public void CloseDialog()
        {
            if (_currentDialog != null)
                _currentDialog.Close();
        }

        public static DialogSO GetDialogFromTrait(ITrait trait)
        {
            DialogSO dialog = ScriptableObject.CreateInstance<DialogSO>();
            dialog.fromBot = trait.Sender;
            dialog.text = trait.GetTraitText();
            return dialog;
        }

        public static DialogSO CreateDialog(Bot fromBot, string text)
        {
            DialogSO dialog = ScriptableObject.CreateInstance<DialogSO>();
            dialog.fromBot = fromBot;
            dialog.text = text;
            return dialog;
        }

        private DialogBox _currentDialog;
    }
}