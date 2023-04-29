using System.Collections;
using NUnit.Framework;
using PenguinPlunge.Core;
using UnityEngine;
using UnityEngine.TestTools;

public class LevelScrollTests
{
    [UnityTest]
    public IEnumerator Scroll_ScrollCoroutineTiggeredOnGameStart()
    {
        var levelScrollerObject = new GameObject();
        levelScrollerObject.AddComponent<LevelScroller>();

        GameEvent.Trigger(GameEventType.GameStarted);
        yield return new WaitForSeconds(1f);

        Assert.IsTrue(levelScrollerObject.transform.position.x < 0);
    }

    [UnityTest]
    public IEnumerator OnGameEvent_SpeedSetToZeroOnGameOver()
    {
        var levelScrollerObject = new GameObject();
        var levelScroller = levelScrollerObject.AddComponent<LevelScroller>();

        GameEvent.Trigger(GameEventType.GameStarted);
        yield return new WaitForSeconds(3f);
        GameEvent.Trigger(GameEventType.GameOver);
        yield return new WaitForSeconds(3f);

        float expected = 0;
        float actual = levelScroller.CurrentSpeed;
        Assert.AreEqual(expected, actual, 0.1f);
    }

    [UnityTest]
    public IEnumerator Scroll_AcceleratesOverTime()
    {
        // Arrange
        var levelScrollerObject = new GameObject();
        var levelScroller = levelScrollerObject.AddComponent<LevelScroller>();
        var gameEvent = new GameEvent(GameEventType.GameStarted);

        // Act
        levelScroller.OnEvent(gameEvent);

        // Assert
        yield return new WaitForSeconds(1.0f);
        var speedAfter1Second = levelScroller.CurrentSpeed;
        yield return new WaitForSeconds(1.0f);
        var speedAfter2Seconds = levelScroller.CurrentSpeed;
        Assert.IsTrue(speedAfter2Seconds > speedAfter1Second);
    }
}
