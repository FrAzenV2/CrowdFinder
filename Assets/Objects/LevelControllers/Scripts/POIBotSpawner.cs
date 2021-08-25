using System;
using System.Collections;
using System.Collections.Generic;
using Bots_Configs.ScriptableObjectConfig;
using Objects.Bots.Scripts;
using Objects.LevelControllers.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objects.LevelControllers
{
    public class POIBotSpawner : LevelBotSpawner
    {
        [SerializeField] protected int _amountToSpawn;
        [SerializeField] protected PoiSpawnDistribution[] _spawnDistributions;

        protected override void Awake()
        {
            SortSpawnDistribution();
            base.Awake();
        }

        protected override void SpawnBots()
        {
            var botsList = new List<BotConfig>(_botsSet.BotConfigs);
            _bots = new List<Bot>();
            foreach (var poiSpawn in _spawnDistributions)
            {
                var poiSpawnAmount = _amountToSpawn * poiSpawn.Distribution;
                for (var i = 0; i < poiSpawnAmount; i++)
                {
                    var offset = CalculateSpawnOffset(poiSpawn);

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
            offset.x = Mathf.Sign(offset.x) * Mathf.Clamp(Math.Abs(offset.x) + poiSpawn.MinDistanceOffset,
                poiSpawn.MinDistanceOffset,
                poiSpawn.MaxDistanceOffset);
            offset.y = Mathf.Sign(offset.y) * Mathf.Clamp(Math.Abs(offset.y) + poiSpawn.MinDistanceOffset,
                poiSpawn.MinDistanceOffset,
                poiSpawn.MaxDistanceOffset);
            return offset;
        }

        private void SortSpawnDistribution()
        {
            Array.Sort(_spawnDistributions);
        }
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