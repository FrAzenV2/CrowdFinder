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

        private void Awake()
        {
            _dialogEventChannel.OnDialogOpened += OpenDialog;
            _dialogEventChannel.OnDialogClosed += CloseDialog;
        }

        public void OpenDialog(DialogSO dialog)
        {
            
            var canvasPosition = Camera.main.WorldToScreenPoint(dialog.fromBot.dialogPoint.transform.position);
            DialogBox dialogBox = Instantiate(_dialogBoxPrefab, canvasPosition , Quaternion.identity, _dialogParent);
            dialogBox.Initialize(dialog);
            ClampToScreen(_dialogParent.GetComponent<RectTransform>(), dialogBox.GetComponent<RectTransform>());
            _currentDialog = dialogBox;
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

        private void ClampToScreen(RectTransform canvas, RectTransform obj)
        {
            var sizeDelta = canvas.sizeDelta - obj.sizeDelta;
            var objPivot = obj.pivot;
            var position = obj.anchoredPosition;
            position.x = Mathf.Clamp(position.x, -sizeDelta.x * objPivot.x, sizeDelta.x * (1 - objPivot.x));
            position.y = Mathf.Clamp(position.y, -sizeDelta.y * objPivot.y, sizeDelta.y * (1 - objPivot.y));
            obj.anchoredPosition = position;
        }

        private DialogBox _currentDialog;
    }
}