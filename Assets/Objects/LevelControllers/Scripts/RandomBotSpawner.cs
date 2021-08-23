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
                List<Vector2> sampledPositions = Gists.FastPoissonDiskSampling.Sampling(spawnRegion.bottomLeftCorner,
                        spawnRegion.topRightCorner, spawnRegion.minDistance, iterationsPerPoint);
                Assert.IsTrue(botsList.Count >= sampledPositions.Count);
                for (int i = 0; i < sampledPositions.Count; i++){
                    var newBot = Instantiate(_botPrefab, sampledPositions[i],
                            Quaternion.identity, _botsParent);
                    
                    var botConfigIndex = Random.Range(0, botsList.Count);
                    newBot.Initialize(botsList[botConfigIndex]);
                    botsList.RemoveAt(botConfigIndex);
                    _bots.Add(newBot);
                }
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            if (spawnRegions != null){
                foreach (var spawnRegion in spawnRegions){
                    Gizmos.DrawWireCube(spawnRegion.GetCenter(), spawnRegion.GetSize());
                }
            }
        }
    }

    [Serializable]
    public class SpawnRegion
    {
        public Vector2 bottomLeftCorner;
        public Vector2 topRightCorner;
        public float minDistance = 1.0f;

        public Vector2 GetCenter(){
            return 0.5f * (topRightCorner + bottomLeftCorner);
        }

        public Vector2 GetSize(){
            return topRightCorner - bottomLeftCorner;
        }
    }
}