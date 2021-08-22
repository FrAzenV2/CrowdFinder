using System;
using System.Collections.Generic;
using Bots_Configs.ScriptableObjectConfig;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objects.LevelControllers
{
    public class LevelBotSpawner : MonoBehaviour
    {
        [SerializeField] private int _amountToSpawn;
        [SerializeField] private Transform _botsParent;
        
        [SerializeField] private PoiSpawnDistribution[] _spawnDistributions;
        [SerializeField] private BotsSet _botsSet;
        [SerializeField] private Bot.Scripts.Bot _botPrefab;

        private void Awake()
        {
            SortSpawnDistribution();
            SpawnBots();
        }

        private void SpawnBots()
        {
            var botsList = new List<BotConfig>(_botsSet.BotConfigs);
            _bots = new List<Bot.Scripts.Bot>();
            foreach (var poiSpawn in _spawnDistributions)
            {
                var poiSpawnAmount = _amountToSpawn * poiSpawn.Distribution;
                for (int i = 0; i < poiSpawnAmount; i++)
                {
                    Vector3 offset = CalculateSpawnOffset(poiSpawn);
                    
                    var newBot = Instantiate(_botPrefab, poiSpawn.PointOfInterest.transform.position + offset,
                        Quaternion.identity, _botsParent);
                    
                    var botConfigIndex = Random.Range(0, botsList.Count);
                    newBot.Initialize(botsList[botConfigIndex]);
                    botsList.RemoveAt(botConfigIndex);
                    _bots.Add(newBot);
                }
            }
        }

        private Vector3 CalculateSpawnOffset(PoiSpawnDistribution poiSpawn)
        {
            Vector3 offset = Random.insideUnitCircle;
            offset *= poiSpawn.MaxDistanceOffset - poiSpawn.MinDistanceOffset;
            offset.x = Mathf.Sign(offset.x)*Mathf.Clamp(Math.Abs(offset.x)+poiSpawn.MinDistanceOffset,
                poiSpawn.MinDistanceOffset,
                poiSpawn.MaxDistanceOffset);
            offset.y = Mathf.Sign(offset.y)*Mathf.Clamp(Math.Abs(offset.y)+poiSpawn.MinDistanceOffset,
                poiSpawn.MinDistanceOffset,
                poiSpawn.MaxDistanceOffset);
            return offset;
        }
        
        private void SortSpawnDistribution()
        {
            Array.Sort(_spawnDistributions);
        }

        private List<Bot.Scripts.Bot> _bots;
    }

    [Serializable]
    public class PoiSpawnDistribution : IComparable<PoiSpawnDistribution>
    {
        public POI PointOfInterest;
        [Range(0, 1)] public float Distribution;
        public float MinDistanceOffset;
        public float MaxDistanceOffset;

        public int CompareTo(PoiSpawnDistribution other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var distributionComparison = Distribution.CompareTo(other.Distribution);
            return distributionComparison;
        }
    }
}