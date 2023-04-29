using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using PenguinPlunge.Core;
using PenguinPlunge.Utility;
using UnityEngine;
using UnityEngine.TestTools;

public class ScoreCounterTests
{
    [Test]
    public void CountScore_CorrectlyCounts()
    {
        var counterObj = Resources.Load<GameObject>("RuntimePrefabs/ScoreCounterPrefab");
        var instantiatedCounterPrefab = GameObject.Instantiate(counterObj).AddComponent<ScoreCounter>();
        instantiatedCounterPrefab.CountScore();

        int randomMovementA = Random.Range(1, 10);
        instantiatedCounterPrefab.transform.position -= new Vector3(randomMovementA, 0, 0);
        instantiatedCounterPrefab.CountScore();
        int randomMovementB = Random.Range(1, 10);
        instantiatedCounterPrefab.transform.position -= new Vector3(randomMovementB, 0, 0);
        instantiatedCounterPrefab.CountScore();

        int expectedScore = randomMovementA + randomMovementB;
        int actualScore = instantiatedCounterPrefab.Score;

        Assert.AreEqual(expectedScore, actualScore);
    }

    [UnityTest]
    public IEnumerator CountWhileMoving_CountsCorreclty() //Think of better name
    {
        var counterObj = Resources.Load<GameObject>("RuntimePrefabs/ScoreCounterPrefab");
        var instantiatedCounterPrefab = GameObject.Instantiate(counterObj).AddComponent<LevelScroller>().gameObject.AddComponent<ScoreCounter>();

        GameEvent.Trigger(GameEventType.GameStarted);
        yield return new WaitForSeconds(2f);
        Assert.IsTrue(instantiatedCounterPrefab.Score != 0);
    }
}
