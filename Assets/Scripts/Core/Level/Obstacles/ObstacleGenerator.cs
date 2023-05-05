using PenguinPlunge.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PenguinPlunge.Core
{
    public class ObstacleGenerator : MonoBehaviour, TEventListener<GameEvent>
    {
        [SerializeField]
        private BaseSpawner[] spawners;

        private BaseSpawner currentSpawner;

        public void Update()
        {
            if (currentSpawner == null) return;

            SelectSpawnerAfterCurrentFinished();
        }

        private void SelectSpawnerAfterCurrentFinished()
        {
            if (currentSpawner.IsFinished())
            {
                currentSpawner = SelectObstacle();
                currentSpawner.Spawn();

                TryCombineSpawnerWithSharks();
            }
        }

        private BaseSpawner SelectObstacle() => spawners.GetRandomElementExcluding(currentSpawner);

        private void SetIntialSpawner()
        {
            currentSpawner = spawners.First(obs => obs is JellyfishSpawner);
            currentSpawner.Spawn();
        }

        private void TryCombineSpawnerWithSharks()
        {
            if (currentSpawner is JellyfishSpawner && UnityEngine.Random.value < 0.5)
            {
                spawners.First(spawner => spawner is SharkObstacleSpawner).Spawn();
            }
        }

        public void OnEvent(GameEvent eventData)
        {
            if (eventData.type == GameEventType.GameStarted)
            {
                SetIntialSpawner();
            }
            else
            {
                currentSpawner = null;
            }
        }

        public void OnEnable() => this.Subscribe();

        public void OnDisable() => this.Unsubscribe();
    }
}
