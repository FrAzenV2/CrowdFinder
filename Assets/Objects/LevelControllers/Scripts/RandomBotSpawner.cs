using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Bots_Configs.ScriptableObjectConfig;
using Gists;
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
            var botsList = new List<BotConfig>(_botsSet.BotConfigs);
            _bots = new List<Bot.Scripts.Bot>();

            foreach (var spawnRegion in spawnRegions){
                List<Vector2> sampledPositions = FastPoissonDiskSampling.Sampling(spawnRegion.GetBottomLeftCorner(),
                        spawnRegion.GetTopRightCorner(), spawnRegion.minDistance, iterationsPerPoint);
                sampledPositions.Shuffle();
                int spawnCount = Mathf.Min(sampledPositions.Count, spawnRegion.maxSpawnAmount);
                Assert.IsTrue(botsList.Count >= spawnCount);
                for (int i = 0; i < spawnCount; i++){
                    var newBot = Instantiate(_botPrefab, sampledPositions[i],
                            Quaternion.identity, _botsParent);
                    
                    var botConfigIndex = Random.Range(0, botsList.Count);
                    newBot.Initialize(botsList[botConfigIndex]);
                    botsList.RemoveAt(botConfigIndex);
                    _bots.Add(newBot);
                }
            }
        }

        protected void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            if (spawnRegions != null){
                foreach (var spawnRegion in spawnRegions){
                    Gizmos.DrawWireCube(spawnRegion.center, spawnRegion.size);
                }
            }
        }
    }

    [Serializable]
    public class SpawnRegion
    {
        public Vector2 center;
        public Vector2 size;
        public int maxSpawnAmount = 10;
        public float minDistance = 1.0f;

        public Vector2 GetBottomLeftCorner(){
            return center - size * 0.5f;
        }

        public Vector2 GetTopRightCorner(){
            return center + size * 0.5f;
        }
    }
}