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
        private ObstacleSpawner[] spawners;

        private ObstacleSpawner currentSpawner;

        public void Update()
        {
            if (currentSpawner == null) return;

            SelectSpawnerAfterCurrentFinished();
        }

        private void SelectSpawnerAfterCurrentFinished()
        {
            if (currentSpawner.Finished())
            {
                currentSpawner = SelectObstacle();
                currentSpawner.Spawn();
            }
        }
        
        private ObstacleSpawner SelectObstacle() => spawners.GetRandomElementExcluding(currentSpawner);

        private void SetIntialSpawner()
        {
            currentSpawner = spawners.First(obs => obs is JellyfishSpawner);
            currentSpawner.Spawn();
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
