using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using PenguinPlunge.Core;
using PenguinPlunge.Utility;

namespace PenguinPlunge.Tests
{
    public class PlayerSpawnerTests
    {
        [Test]
        public void Spawn_PlayerInstantiated()
        {
            var obj = new GameObject().AddComponent<PlayerSpawner>();
            obj.SpawnPlayer();
            Assert.IsNotNull(obj);
        }

        [UnityTest]
        public IEnumerator SpawnPlayerAfterFeedbacks_SpawnsPlayer()
        {
            var obj = new GameObject();
            var spawner = obj.AddComponent<PlayerSpawner>();
            float maximumSpawnDelay = 3f; //Highly Likely Value Will Not Exceed 3 As The Game Starts Quickly

            spawner.SpawnPlayerAfterFeedbacks().StartAsCoroutine();
            yield return new WaitForSeconds(maximumSpawnDelay);
            Assert.IsNotNull(GameObject.FindGameObjectWithTag("Player"));
        }
    }
}
