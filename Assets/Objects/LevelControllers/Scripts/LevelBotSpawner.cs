using System;
using System.Collections.Generic;
using Bots_Configs.ScriptableObjectConfig;
using UnityEngine;


namespace Objects.LevelControllers
{
    public class LevelBotSpawner : MonoBehaviour
    {
        [SerializeField] protected int _amountToSpawn;
        [SerializeField] protected Transform _botsParent;
        
        [SerializeField] protected BotsSet _botsSet;
        [SerializeField] protected Bot.Scripts.Bot _botPrefab;

        protected List<Bot.Scripts.Bot> _bots;

        protected virtual void Awake()
        {
            SpawnBots();
        }

        protected virtual void SpawnBots()
        {
           
        }

    }
}