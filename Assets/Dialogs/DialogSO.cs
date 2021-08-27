using System.Collections.Generic;
using UnityEngine;
using Objects.Bots.Scripts;

namespace Dialogs
{
    [CreateAssetMenu(fileName = "newDialog", menuName = "Dialogs/New Dialog", order = 0)]
    public class DialogSO : ScriptableObject
    {
        public string text;
        public Bot fromBot;
    }
}