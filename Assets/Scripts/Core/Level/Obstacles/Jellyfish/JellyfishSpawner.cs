using PenguinPlunge.Pooling;
using PenguinPlunge.Utility;
using Sirenix.OdinInspector;
using UnityEngine;
using PenguinPlunge.Data;
using System.Collections.Generic;

namespace PenguinPlunge.Core
{
    public class ObjectLayout
    {
        public Vector2 Offset => position;
        public float Rotation => rotation;
        public Size Size => size;

        [SerializeField] 
        private Vector2 position;
        [SerializeField] 
        private float rotation;
        [SerializeField]
        private Size size;

        public ObjectLayout(Vector2 position, float rotation, Size size)
        {
            this.position = position;
            this.rotation = rotation;
            this.size = size;
        }
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
        private List<ObjectLayout[]> fixedLayouts;

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
            obstaclePool = new(obstaclePrefab, 15, parentTransform);
        }

        public void AddPremadeLayout(ObjectLayout[] layout)
        {
            fixedLayouts.Add(layout);
        }

        [Button("Spawn Premade Layout")]
        private void SpawnPremadeLayout(int index)
        {
            SpawnFixedLayout(index);
        }    
        private int jellyfishObstacleCount;
        private int spawnedCount;
        public override void Spawn()
        {
            jellyfishObstacleCount = Random.Range(6, 20);
            CreateFirst();
            for (int i = 0; i < jellyfishObstacleCount; i++)
            {
                int index = Random.Range(0, fixedLayouts.Count);
                if (Random.value > 0.25f || i == 0)
                {
                    SetJellyfishPositionX();
                    recentSpawn.DisableWhenPassedAndOutOfSight().StartAsCoroutine();
                    SetJellyfishHeightAndRotation();
                    spawnedCount++;
                }
                else if (fixedLayouts[index].Length <= jellyfishObstacleCount - i)
                {
                    spawnedCount += fixedLayouts[index].Length;
                    i += fixedLayouts[index].Length - 1;
                    SpawnFixedLayout(index);
                }
            }
            ReduceSpawnDistance();

            void CreateFirst()
            {
                recentSpawn = obstaclePool.Get();
                float spawnX = transform.position.x + potentialDistanceApart.RandomInRange() + 50f;
                recentSpawn.transform.SetX(spawnX);
                recentSpawn.DisableWhenPassedAndOutOfSight().StartAsCoroutine();
                SetJellyfishHeightAndRotation();
            }

        }

        private void SpawnFixedLayout(int index)
        {
            ObjectLayout[] layouts = fixedLayouts[index];
            float startX = recentSpawn.transform.position.x + potentialDistanceApart.RandomInRange();
            foreach (var layout in layouts)
            {
                recentSpawn = obstaclePool.Get();
                float spawnX = startX + layout.Offset.x;
                recentSpawn.transform.SetX(spawnX);
                recentSpawn.SetToMatchPremadeLayout(layout.Offset.y, layout.Rotation, layout.Size);
                recentSpawn.DisableWhenPassedAndOutOfSight().StartAsCoroutine();
            }
        }

        private void SetJellyfishPositionX()
        {
            float spawnX = recentSpawn.transform.position.x;
            spawnX += potentialDistanceApart.RandomInRange();
            recentSpawn = obstaclePool.Get();
            recentSpawn.transform.position = new Vector3(spawnX, recentSpawn.transform.position.y, transform.position.z);
        }

        private void SetJellyfishHeightAndRotation()
        {
            currentObstaclePos = currentObstaclePos.GetRandomEnumValueExcluding();
            currentObstacleSize = currentObstacleSize.GetRandomEnumValueExcluding();
            recentSpawn.SetToMatchPosition(currentObstaclePos, currentObstacleSize);
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