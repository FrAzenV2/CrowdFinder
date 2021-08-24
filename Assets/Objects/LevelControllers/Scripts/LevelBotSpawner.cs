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


        public Bot TargetBot => _targetBot;
        public IReadOnlyList<Bot> FakeTargets => _fakeTargets;
        
        protected Bot _targetBot;
        protected List<Bot> _fakeTargets;
        protected List<Bot> _bots;

        protected virtual void Awake()
        {
            SpawnBots();
        }

        protected virtual void SpawnBots()
        {
            _fakeTargets = new List<Bot>();
            _bots = new List<Bot>();
        }
    }
}