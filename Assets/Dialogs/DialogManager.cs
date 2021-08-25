using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Bots.Scripts;
using EventChannels;
using Dialogs;

namespace Managers
{
    public class DialogManager : MonoBehaviour
    {
        //[SerializeField] private TraitEventChannelSO _traitEventChannel = default;
        [SerializeField] private DialogEventChannelSO _dialogEventChannel = default;
        [SerializeField] private DialogBox _dialogBoxPrefab;
        [SerializeField] private Transform _dialogParent;
        
        // Start is called before the first frame update
        private void Awake()
        {
            _dialogEventChannel.OnDialogOpened += OpenDialog;
            _dialogEventChannel.OnDialogClosed += CloseDialog;
        }

        public void OpenDialog(DialogSO dialog)
        {
            var canvasPosition = Camera.main.WorldToScreenPoint(dialog.fromBot.transform.position);
            DialogBox dialogBox = Instantiate(_dialogBoxPrefab, canvasPosition , Quaternion.identity, _dialogParent);
            dialogBox.Initialize(dialog);
            _currentDialog = dialogBox;
        }

        public void CloseDialog()
        {
            if (_currentDialog != null)
                _currentDialog.Close();
        }

        private DialogBox _currentDialog;
    }
}