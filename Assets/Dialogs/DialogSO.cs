using System.Collections.Generic;
using UnityEngine;
using Objects.Bots.Scripts;

namespace Dialogs
{
    public class DialogSO : ScriptableObject
    {
        public string text;
        public Bot fromBot;
    }
}