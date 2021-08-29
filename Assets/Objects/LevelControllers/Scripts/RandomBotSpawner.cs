using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Bots_Configs.ScriptableObjectConfig;
using Gists;
using Objects.Bots.Scripts;
using Objects.LevelControllers.Scripts;
using Random = UnityEngine.Random;

namespace Objects.LevelControllers
{
    public class RandomBotSpawner : LevelBotSpawner
    {
        [SerializeField] protected SpawnRegion[] spawnRegions;
        [SerializeField] protected int iterationsPerPoint = 30;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void SpawnBots()
        {
            base.SpawnBots();
            print("Spawning bots " + _targetBot);
            var botsList = new List<BotConfig>(_botsSet.BotConfigs);
            botsList.Shuffle();

            spawnRegions.Shuffle();

            foreach (var spawnRegion in spawnRegions)
            {
                var sampledPositions = FastPoissonDiskSampling.Sampling(spawnRegion.GetBottomLeftCorner(),
                    spawnRegion.GetTopRightCorner(), spawnRegion.minDistance, iterationsPerPoint);
                sampledPositions.Shuffle();
                
                var spawnCount = Mathf.Min(sampledPositions.Count, spawnRegion.maxSpawnAmount);
                
                Assert.IsTrue(botsList.Count >= spawnCount);
                
                for (var i = 0; i < spawnCount; i++)
                {
                    var botConfigIndex = Random.Range(0, botsList.Count);
                    var newBot = Instantiate(_botPrefab, sampledPositions[i],
                        Quaternion.identity, _botsParent);
                    var config = ScriptableObject.CreateInstance<BotConfig>();
                    config.Initialize(botsList[botConfigIndex]);
                    newBot.Initialize(config);
                    if (_targetBot==null)
                    {
                        _targetBot = newBot;
                        if (_targetNameOverride != "")
                            _targetBot.Config.BotName = _targetNameOverride;
                        _targetBot.IsTarget = true;
                    }
                    else
                    {
                        _bots.Add(newBot);
                    }
                    botsList.RemoveAt(botConfigIndex);
                }
            }

            for (var i = 0; i < _fakeTargetsAmount; i++)
            {
                var fakeTarget = _bots[Random.Range(0, _bots.Count-1)];
                fakeTarget.IsFakeTarget = true;
                fakeTarget.Config.SetName(_targetBot.Config.BotName);
                _bots.Remove(fakeTarget);
                _fakeTargets.Add(fakeTarget);
            }
            foreach (Bot bot in _fakeTargets)
            {
                print("Fake target bot " + bot);
            }
            print("Spawned bot - correct - target " + _targetBot);
        }

        protected void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            if (spawnRegions != null)
                foreach (var spawnRegion in spawnRegions)
                    Gizmos.DrawWireCube(spawnRegion.center, spawnRegion.size);
        }
    }

    [Serializable]
    public class SpawnRegion
    {
        public Vector2 center;
        public Vector2 size;
        public int maxSpawnAmount = 10;
        public float minDistance = 1.0f;

        public Vector2 GetBottomLeftCorner()
        {
            return center - size * 0.5f;
        }

        public Vector2 GetTopRightCorner()
        {
            return center + size * 0.5f;
        }
    }
}