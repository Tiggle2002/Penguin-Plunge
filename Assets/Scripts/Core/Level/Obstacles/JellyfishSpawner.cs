using PenguinPlunge.Pooling;
using PenguinPlunge.Utility;
using Sirenix.OdinInspector;
using UnityEngine;
using PenguinPlunge.Data;

namespace PenguinPlunge.Core
{
    public struct FixedJellyfishLayout
    {
        public Size jellyfishSize;
        public float height;
        public float offsetX; 
        public float rotation;
    }

    public class JellyfishSpawner : BaseSpawner
    {
        [SerializeField, MinMaxSlider(10f, 100f)]
        private Vector2 potentialDistanceApart;
        [SerializeField, TitleGroup("Distance to Subtract Each Spawn", Alignment=TitleAlignments.Centered), HideLabel]
        private float distanceSubtractionPerSpawn;
        [SerializeField, Range(0f, 1f)]
        private float chanceForADoubleObstacle;

        [SerializeField]
        private GameObject obstaclePrefab;
        [SerializeField]
        private Transform parentTransform;

        private ObjectPool<JellyfishObstacle> obstaclePool;

        private JellyfishObstacle recentSpawn;
        private ObstaclePosition currentObstaclePos;
        private Size currentObstacleSize;

        private const float MinimumDistance = 15f;
        private const float MaximumDistance = 35f;

        public void Awake()
        {
            obstaclePool = new(obstaclePrefab, 10, parentTransform);
        }

        public override void Spawn()
        {
            recentSpawn = obstaclePool.Get();
            float spawnX = transform.position.x + potentialDistanceApart.RandomInRange() + 50f;

            recentSpawn.transform.position = new Vector3(spawnX, recentSpawn.transform.position.y, transform.position.z);
            EnableJellyfish();
            SetCurrentObstacle();

            for (int i = 0; i < 9; i++)
            {
                SetJellyfishPosition();
                EnableJellyfish();
                SetCurrentObstacle();
            }
            ReduceSpawnDistance();
        }

        public void EnableJellyfish()
        {
            recentSpawn.DisableOnTime().StartAsCoroutine();
        }

        private void SetCurrentObstacle()
        {
            currentObstaclePos = currentObstaclePos.GetRandomEnumValueExcluding();
            currentObstacleSize = currentObstacleSize.GetRandomEnumValueExcluding();
            recentSpawn.SetPosition(currentObstaclePos, currentObstacleSize);
        }

        private void SetJellyfishPosition()
        {
            float spawnX = recentSpawn.transform.position.x;
            if (!CanSpawnDoubleOnX())
            {
                spawnX += potentialDistanceApart.RandomInRange();
            }
            recentSpawn = obstaclePool.Get();
            recentSpawn.transform.position = new Vector3(spawnX, recentSpawn.transform.position.y, transform.position.z);
        }

        private void ReduceSpawnDistance()
        {
            potentialDistanceApart -= new Vector2(distanceSubtractionPerSpawn, distanceSubtractionPerSpawn);
            potentialDistanceApart.x = Mathf.Clamp(potentialDistanceApart.x, MinimumDistance, 1000);
            potentialDistanceApart.y = Mathf.Clamp(potentialDistanceApart.y, MaximumDistance, 1000);
        }
        bool doubleJustSpawned = false;
        private bool CanSpawnDoubleOnX()
        {
            if (chanceForADoubleObstacle < Random.value && !doubleJustSpawned)
            {
                doubleJustSpawned = true;
                return true;
            }
            doubleJustSpawned = false;
            return false;
        }

        public override bool IsFinished() => obstaclePool.AtCapacity;
    }
}