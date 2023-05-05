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
        [SerializeField, TitleGroup("Range of Jellyfish that can be Spawned", Alignment = TitleAlignments.Centered), HideLabel]
        private Vector2Int potentialJellyfishCount = new(6, 10);
        [SerializeField, MinMaxSlider(10f, 100f), TitleGroup("Range of Obstacle Spawning Distance", Alignment=TitleAlignments.Centered)]
        private Vector2 potentialDistanceApart;
        [SerializeField, TitleGroup("Distance to Subtract Each Spawn Cycle", Alignment=TitleAlignments.Centered), HideLabel]
        private float distanceToSubtractionEachSpawnCycle;

        [SerializeField]
        private List<ObjectLayout[]> fixedLayouts;

        private ObjectPool<JellyfishObstacle> obstaclePool;

        private JellyfishObstacle recentSpawn;
        private ObstaclePosition currentObstaclePos;
        private Size currentObstacleSize;

        private int JellyfishCountThisCycle => potentialJellyfishCount.RandomInRange();

        private float SpawnerPosition => transform.position.x;

        private float SpawnPosX
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

        private readonly Vector2 MinDistanceApartRange = new Vector2(15f, 35f);
        private readonly int MaxJellyfishCount = 20;

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

        public void AddPremadeLayout(ObjectLayout[] layout)
        {
            fixedLayouts.Add(layout);
        }

        [Button("Spawn Premade Layout")]
        private void SpawnPremadeLayout(int index)
        {
            SpawnFixedLayout(index);
        }

        [Button("Spawn")]
        public override void Spawn()
        {
            int jellyfishObstacleCount = JellyfishCountThisCycle;
            for (int i = 0; i < jellyfishObstacleCount; i++)
            {
                int remainingJellyfishToSpawn = jellyfishObstacleCount - i;

                int index = Random.Range(0, fixedLayouts.Count);
                if (Random.value > 0.25f)
                {
                    SetJellyfishPositionX();
                    recentSpawn.DisableWhenPassedAndOutOfSight().StartAsCoroutine();
                    SetJellyfishHeightAndRotation(recentSpawn);
                }
                else
                {
                    //i += fixedLayouts[index].Length - 1;
                    SpawnFixedLayout(index);
                }
            }
            recentSpawn = null;
            IncreasePotentialJellyfishCount();
            ReduceSpawnDistance();

            //List<ObjectLayout[]> GetLayoutsBelowAllowRemainingJellyfish(int maxCountRemaining)
            //{
            //    List<ObjectLayout[]> potentialLayouts = fixedLayouts.Where(layout => layout.Length <= maxCountRemaining).ToList();

            //    return potentialLayouts.Count > 0;
            //}

            //void SpawnJellyfishOrLayout()
            //{
            //    int index = Random.Range(0, fixedLayouts.Count);
            //    if (Random.value > 0.25f)
            //    {
            //        SetJellyfishPositionX();
            //        recentSpawn.DisableWhenPassedAndOutOfSight().StartAsCoroutine();
            //        SetJellyfishHeightAndRotation(recentSpawn);
            //    }
            //    else if (fixedLayouts[index].Length <= jellyfishObstacleCount - i)
            //    {
            //        i += fixedLayouts[index].Length - 1;
            //        SpawnFixedLayout(index);
            //    }
            //}

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



        private void CarryOutSpawnCycle()
        {

        }

        private void SpawnFixedLayout(int index)
        {
            ObjectLayout[] layouts = fixedLayouts[index];
            float startX = SpawnPosX;
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
            float PositionX = SpawnPosX;
            recentSpawn = obstaclePool.Get();
            recentSpawn.transform.SetX(PositionX);
        }

        private void SetJellyfishHeightAndRotation(JellyfishObstacle jellyfish)
        {
            currentObstaclePos = currentObstaclePos.GetRandomEnumValueExcluding();
            currentObstacleSize = currentObstacleSize.GetRandomEnumValueExcluding();
            jellyfish.SetToMatchPosition(currentObstaclePos, currentObstacleSize);
        }



        public override bool IsFinished() => obstaclePool.AtCapacity;
    }
}