using PenguinPlunge.Utility;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace PenguinPlunge.Core
{
    [RequireComponent(typeof(LevelScroller))]
    public class ScoreCounter : SingletonMonoBehaviour<ScoreCounter>, TEventListener<GameEvent>
    {
        public int HighScore => PlayerPrefs.GetInt("highscore", 0);
        public int Score => scoreCount;

        int scoreCount = 0;
        LevelScroller scroller;

        public override void Awake()
        {
            base.Awake();
            scroller = GetComponent<LevelScroller>();
            ResetFishCount();

            void ResetFishCount() => PlayerPrefs.SetInt("fishCount", 0); 
        }

        public IEnumerator CountWhileMoving()
        {
            do
            {
                CountScore();
                yield return null;
            }
            while (scroller.CurrentSpeed != 0);
        }

        public void CountScore()
        {
            int newScore = GetScore();
            if (newScore <= scoreCount) return;

            scoreCount = newScore;
            if (scoreCount > HighScore) 
            {
                PlayerPrefs.SetInt("highscore", scoreCount);
            }
        }

        public int GetScore()
        {
            float absoluteDistance = Mathf.Abs(0 - transform.position.x);
            return Mathf.RoundToInt(absoluteDistance); 
        }

        public void OnEvent(GameEvent eventData)
        {
            if (eventData.type == GameEventType.GameStarted)
            {
                StartCoroutine(CountWhileMoving());
            }
        }

        public void OnEnable() => this.Subscribe();

        public void OnDisable() => this.Unsubscribe();
    }
}
