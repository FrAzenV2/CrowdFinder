using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventChannels;
using Dialogs;

namespace Managers
{
    public class DialogManager : MonoBehaviour
    {
        //[SerializeField] private TraitEventChannelSO _traitEventChannel = default;
        [SerializeField] private DialogEventChannelSO _dialogEventChannel = default;

        // Start is called before the first frame update
        private void Awake()
        {
            _dialogEventChannel.OnDialogOpened = OpenDialog;
            _dialogEventChannel.OnDialogClosed = CloseDialog;
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public void OpenDialog(DialogSO dialog)
        {
            // Create a new trait based on bot data and assign it
            //bot.AssignTrait()
        }

        public void CloseDialog()
        {
        }
    }
}