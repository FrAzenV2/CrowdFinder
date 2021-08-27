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

        private void Awake() {
            _animator = GetComponent<Animator>();
        }
        public void Initialize(DialogSO dialog){
            _dialog = dialog;
            _textLabel.text = $"{dialog.fromBot.Config.BotName}:\n{dialog.text}";
        }

        public void Close(){
            _animator.Play("Hide");
        }

        public void Remove(){
            Destroy(gameObject);
        }

        private DialogSO _dialog;
        private Animator _animator;
    }
}