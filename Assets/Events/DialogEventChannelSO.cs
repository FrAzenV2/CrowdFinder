using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Objects.Bot.Scripts;
using Dialogs;

namespace EventChannels
{
    public class DialogEventChannelSO : ScriptableObject
    {
        public UnityAction<DialogSO> OnDialogOpened;
        public UnityAction OnDialogClosed;
        
        public void OpenDialog(DialogSO dialog)
        {
            OnDialogOpened.Invoke(dialog);
        }

        public void CloseDialog()
        {
            OnDialogClosed.Invoke();
        }
    }
}
