using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogs;
using TMPro;

namespace Dialogs
{
    public class DialogBox : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textLabel;
        public void Initialize(DialogSO dialog){
            _dialog = dialog;
            _textLabel.text = $"{dialog.fromBot.Config.BotName}:\n{dialog.text}";
        }

        public void Close(){
            Destroy(gameObject);
        }

        private DialogSO _dialog;
    }
}