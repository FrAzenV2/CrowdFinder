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
        private void Awake()
        {   
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void Initialize(DialogSO dialog){
            _dialog = dialog;
            _textLabel.text = dialog.text;
        }

        public void Close(){
            Destroy(gameObject);
        }

        private DialogSO _dialog;
    }
}