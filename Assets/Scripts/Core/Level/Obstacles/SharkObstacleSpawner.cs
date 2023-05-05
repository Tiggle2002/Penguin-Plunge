using PenguinPlunge.Pooling;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace PenguinPlunge.Core
{
    public class SharkObstacleSpawner : BaseSpawner
    {
        [SerializeField]
        private float sharkHorizontalSpeed;
        [SerializeField]
        private float sharkVerticalSpeed;
        [SerializeField]
        private float speedIncrease;
        [SerializeField]
        private float verticalSpeedIncrease;
        [SerializeField]
        private float maxSpeed;
        private GameObject obstaclePrefab;
        private ObjectPool<SharkObstacle> objectPool;
        private float timeBetweenSpawns = 2f;

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
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
            IncreaseSpeed();
            DecreaseTimeBetweenSpawns();
        }

        private void ActivateShark()
        {
            var sharkObj = objectPool.Get();
            sharkObj.SetSpeed(sharkHorizontalSpeed, sharkVerticalSpeed);
        }

        private void IncreaseSpeed()
        {
            sharkHorizontalSpeed += speedIncrease;
            sharkVerticalSpeed += verticalSpeedIncrease;
            sharkHorizontalSpeed = Mathf.Clamp(sharkHorizontalSpeed, 0, maxSpeed);
            sharkVerticalSpeed = Mathf.Clamp(sharkVerticalSpeed, 0, maxSpeed);
        }

        private void DecreaseTimeBetweenSpawns()
        {
            timeBetweenSpawns -= 0.1f;
            timeBetweenSpawns = Mathf.Clamp(timeBetweenSpawns, 0.5f, 1.5f);
        }

        public override bool IsFinished() => objectPool.AtCapacity;
    }
}