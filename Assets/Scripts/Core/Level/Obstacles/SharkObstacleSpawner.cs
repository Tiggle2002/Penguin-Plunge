using Codice.CM.Common;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using PenguinPlunge.Data;
using PenguinPlunge.Pooling;
using PenguinPlunge.Utility;
using Sirenix.OdinInspector;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PenguinPlunge.Core
{
    public class SharkObstacleSpawner : ObstacleSpawner
    {
        [SerializeField]
        private float sharkHorizontalSpeed;
        [SerializeField]
        private float sharkVerticalSpeed;
        private GameObject obstaclePrefab;
        private ObjectPool<SharkObstacle> objectPool;

        public void Awake()
        {
            obstaclePrefab = Resources.Load<GameObject>("RuntimePrefabs/Obstacles/SharkObstacle");
            objectPool = new(obstaclePrefab, 10, transform);
        }

        [Button("Spawn")]
        public override void Spawn() => StartCoroutine(GenerateRandomSharkSequence());

        private IEnumerator GenerateRandomSharkSequence()
        {
            int sharkCount = Random.Range(1, 5);
            for (int i = 0; i < sharkCount; i++)
            {
                ActivateShark();
                yield return new WaitForSeconds(Random.Range(1f, 2f));
            }
        }

        private void ActivateShark()
        {
            var sharkObj = objectPool.Get();
            sharkObj.SetSpeed(sharkHorizontalSpeed, sharkVerticalSpeed);
        }

        public override bool Finished() => objectPool.AtCapacity;
    }
}