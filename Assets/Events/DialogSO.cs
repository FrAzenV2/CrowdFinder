using System.Collections.Generic;
using UnityEngine;

namespace Dialogs
{
    [CreateAssetMenu(fileName = "newDialog", menuName = "Dialog/Dialog Data")]
    public class DialogSO : ScriptableObject
    {
        public string dialogText;
    }
}