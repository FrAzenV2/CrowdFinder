using System.Collections.Generic;
using UnityEngine;

namespace Bots_Configs.ScriptableObjectConfig
{
    [CreateAssetMenu(fileName = "New Bots Set", menuName = "Bots/Bots Set", order = 0)]
    public class BotsSet : ScriptableObject
    {
        public List<BotConfig> BotConfigs;
    }
}