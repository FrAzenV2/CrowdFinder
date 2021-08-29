using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Dialogs;

namespace EventChannels
{
    [CreateAssetMenu(fileName = "newDialogEventChannel", menuName = "Events/Dialog Event Channel")]
    public class DialogEventChannelSO : ScriptableObject
    {
        public UnityAction<DialogSO> OnDialogOpened;
        public UnityAction<DialogSO> OnDialogUpdated;
        public UnityAction OnDialogClosed;

        public void OpenDialog(DialogSO dialog)
        {
            if (OnDialogOpened != null)
                OnDialogOpened.Invoke(dialog);
        }

        public void UpdateDialog(DialogSO dialog)
        {
            if (OnDialogUpdated != null)
                OnDialogUpdated.Invoke(dialog);
        }

        public void CloseDialog()
        {
            if (OnDialogClosed != null)
                OnDialogClosed.Invoke();
        }
    }
}