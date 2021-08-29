using System.Collections.Generic;
using Bots_Configs.ScriptableObjectConfig;
using Objects.Bots.Scripts;
using UnityEngine;

namespace Objects.LevelControllers.Scripts
{
    public class LevelBotSpawner : MonoBehaviour
    {
        [SerializeField] protected Transform _botsParent;
        [SerializeField] protected int _fakeTargetsAmount = 2;
        [SerializeField] protected BotsSet _botsSet;
        [SerializeField] protected Bot _botPrefab;
        [SerializeField] protected string _targetNameOverride;


        public Bot TargetBot => _targetBot;
        public IReadOnlyList<Bot> FakeTargets => _fakeTargets;
        public IReadOnlyList<Bot> Bots => _bots;
        
        protected Bot _targetBot;
        protected List<Bot> _fakeTargets;
        protected List<Bot> _bots;

        protected virtual void Awake()
        {
            print("Spawning bots!");
            SpawnBots();
            print(_bots.Count);
        }

        protected virtual void SpawnBots()
        {
            _targetBot = null;
            _fakeTargets = new List<Bot>();
            _bots = new List<Bot>();
        }
    }
}