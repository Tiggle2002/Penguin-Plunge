using PenguinPlunge.Pooling;
using PenguinPlunge.Utility;
using Sirenix.OdinInspector;
using UnityEngine;
using PenguinPlunge.Data;
using System.Collections.Generic;
using System.Linq;

namespace PenguinPlunge.Core
{
    public class JellyfishSpawner : BaseSpawner
    {
        [SerializeField, MinMaxSlider(5f, 30f), TitleGroup("Range of Jellyfish that can be Spawned", Alignment = TitleAlignments.Centered), HideLabel]
        private Vector2Int potentialJellyfishCount = new(6, 10);

        [SerializeField, MinMaxSlider(10f, 100f), TitleGroup("Range of Obstacle Spawning Distance", Alignment=TitleAlignments.Centered), HideLabel]
        private Vector2 potentialDistanceApart;

        [SerializeField, Range(1f, 10f), TitleGroup("Distance Between Jellyfish to Subtract Each Spawn Cycle", Alignment=TitleAlignments.Centered), HideLabel]
        private float distanceToSubtractionEachSpawnCycle;

        [SerializeField, Required]
        private JellyfishLayoutData fixedLayoutData;

        private ObjectPool<JellyfishObstacle> obstaclePool;
        private JellyfishObstacle recentSpawn;
        private ObstaclePosition currentObstaclePos;
        private Size currentObstacleSize;

        private readonly Vector2 MinDistanceApartRange = new Vector2(15f, 35f);
        private readonly int MaxJellyfishCount = 20;

        private int JellyfishCountThisCycle => potentialJellyfishCount.RandomInRange();

        private float SpawnerPosition => transform.position.x;

        private float SpawnPosXAccordingToRecentSpawn
        {
            get
            {
                if(recentSpawn == null)
                {
                    float initialPositionBuffer = CameraFunctions.ScreenWidth / 2;
                    return SpawnerPosition + potentialDistanceApart.RandomInRange() + initialPositionBuffer;
                }
                return recentSpawn.transform.position.x + potentialDistanceApart.RandomInRange();
            }
        }

        public void Awake()
        {
            InitialiseJellyfishPool();

            void InitialiseJellyfishPool()
            {
                GameObject obstaclePrefab = Resources.Load<GameObject>("RuntimePrefabs/Obstacles/Jellyfish");
                Transform parent = GameObject.FindObjectOfType<LevelScroller>().transform;
                obstaclePool = new(obstaclePrefab, 15, parent);
            }
        }

        [Button("Spawn Premade Layout")]
        private void SpawnPremadeLayout()
        {
            if (fixedLayoutData.CanSpawnADifferentLayout())
            {
                SpawnFixedLayout();
            }
            else
            {
                Debug.Log("Jellyfish Spawner has no Available Layouts!");
            }
        }

        [Button("Spawn")]
        public override void Spawn()
        {
            int jellyfishObstacleCount = JellyfishCountThisCycle;
            for (int i = 0; i < jellyfishObstacleCount; i++)
            {
                if (fixedLayoutData.CanSpawnADifferentLayout() && Random.value < 0.25f)
                {
                    SpawnFixedLayout();
                }
                else
                {
                    SpawnIndividualJellyfish();
                }
            }
            recentSpawn = null;
            IncreasePotentialJellyfishCount();
            ReduceSpawnDistance();

            void IncreasePotentialJellyfishCount()
            {
                potentialDistanceApart.y += 1;
                potentialDistanceApart.y = Mathf.Clamp(potentialDistanceApart.y, 0, MaxJellyfishCount);
            }

            void ReduceSpawnDistance()
            {
                potentialDistanceApart -= new Vector2(distanceToSubtractionEachSpawnCycle, distanceToSubtractionEachSpawnCycle);
                potentialDistanceApart.x = Mathf.Clamp(potentialDistanceApart.x, MinDistanceApartRange.x, 1000);
                potentialDistanceApart.y = Mathf.Clamp(potentialDistanceApart.y, MinDistanceApartRange.y, 1000);
            }
        }

        private void SpawnIndividualJellyfish()
        {
            SetJellyfishPositionX();
            recentSpawn.DisableWhenPassedAndOutOfSight().StartAsCoroutine();
            SetJellyfishHeightAndRotation(recentSpawn);
        }

        private void SpawnFixedLayout()
        {
            IndividualJellyfishLayout[] layouts = fixedLayoutData.GetRandomDifferentLayoutAccordingToScore();
            float startX = SpawnPosXAccordingToRecentSpawn;
            foreach (var layout in layouts)
            {
                recentSpawn = obstaclePool.Get();
                recentSpawn.transform.SetX(startX);
                recentSpawn.SetToMatchPremadeLayout(layout.Offset, layout.Rotation, layout.Size);
                recentSpawn.DisableWhenPassedAndOutOfSight().StartAsCoroutine();
            }
        }

        private void SetJellyfishPositionX()
        {
            float PositionX = SpawnPosXAccordingToRecentSpawn;
            recentSpawn = obstaclePool.Get();
            recentSpawn.transform.SetX(PositionX);
        }

        private void SetJellyfishHeightAndRotation(JellyfishObstacle jellyfish)
        {
            currentObstaclePos = SelectJellyfishPosition();
            currentObstacleSize = currentObstacleSize.GetRandomEnumValueExcluding();
            jellyfish.SetToMatchPosition(currentObstaclePos, currentObstacleSize);
        }

        int counter = 0;
        private ObstaclePosition SelectJellyfishPosition()
        {
            counter++;
            if (counter % 2 ==0)
            {
                return ObstaclePosition.Bottom;
            }
            else
            {
                return currentObstaclePos.GetRandomEnumValueExcluding();
            }
        }

        public override bool IsFinished() => obstaclePool.AtCapacity;
    }
}