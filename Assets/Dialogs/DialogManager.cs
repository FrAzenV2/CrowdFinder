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
        
        // Start is called before the first frame update
        private void Awake()
        {
            _dialogEventChannel.OnDialogOpened = OpenDialog;
            _dialogEventChannel.OnDialogClosed = CloseDialog;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void OpenDialog(DialogSO dialog)
        {
            print("Opening dialog with text " + dialog.text);
            DialogBox dialogBox = Instantiate(_dialogBoxPrefab, dialog.fromBot.transform.position, Quaternion.identity, dialog.fromBot.transform) as DialogBox;
            dialogBox.Initialize(dialog);
            _currentDialog = dialogBox;
        }

        public void CloseDialog()
        {
            if (_currentDialog == null)
                return;
            print("Closing dialog");
            _currentDialog.Close();
        }

        private DialogBox _currentDialog;
    }
}