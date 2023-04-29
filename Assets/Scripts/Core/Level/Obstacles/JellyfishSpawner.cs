using Codice.Client.BaseCommands.Merge;
using PenguinPlunge.Pooling;
using PenguinPlunge.Utility;
using Sirenix.OdinInspector;
using System.Collections;
using System.Linq;
using UnityEngine;
using PenguinPlunge.Data;

namespace PenguinPlunge.Core
{
    public class JellyfishSpawner : ObstacleSpawner
    {
        [SerializeField, MinMaxSlider(10f, 100f)]
        private Vector2 potentialDistanceApart;
        [SerializeField]
        private GameObject obstaclePrefab;
        [SerializeField]
        private Transform parentTransform;

        private ObjectPool<JellyfishObstacle> obstaclePool;

        private JellyfishObstacle recentSpawn;
        private ObstaclePosition currentObstaclePos;
        private Size currentObstacleSize;

        public void Awake()
        {
            obstaclePool = new(obstaclePrefab, 10, parentTransform);
        }

        public override void Spawn()
        {
            recentSpawn = obstaclePool.Get();
            float spawnX = transform.position.x + potentialDistanceApart.RandomInRange();

            recentSpawn.transform.position = new Vector3(spawnX, recentSpawn.transform.position.y, transform.position.z);
            EnableJellyfish();
            SetCurrentObstacle();

            for (int i = 0; i < 9; i++)
            {
                SetJellyfishPosition();
                EnableJellyfish();
                SetCurrentObstacle();
            }
        }

        public void EnableJellyfish()
        {
            currentObstaclePos = currentObstaclePos.GetRandomEnumValueExcluding();
            currentObstacleSize = currentObstacleSize.GetRandomEnumValueExcluding();
            recentSpawn.DisableOnTime().StartAsCoroutine();
        }

        private void SetCurrentObstacle()
        {
            currentObstaclePos = currentObstaclePos.GetRandomEnumValue();
            currentObstacleSize = currentObstacleSize.GetRandomEnumValue();
            recentSpawn.SetPosition(currentObstaclePos, currentObstacleSize);
        }

        private void SetJellyfishPosition()
        {
            float spawnX = recentSpawn.transform.position.x + potentialDistanceApart.RandomInRange();
            recentSpawn = obstaclePool.Get();
            recentSpawn.transform.position = new Vector3(spawnX, recentSpawn.transform.position.y, transform.position.z);
        }


        public override bool Finished() => obstaclePool.AtCapacity;
    }
}