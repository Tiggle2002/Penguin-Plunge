using PenguinPlunge.Core;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace PenguinPlunge.UI
{
    public class ScoreDisplayer : MonoBehaviour
    {
        TextMeshProUGUI highestScoreTMP;
        TextMeshProUGUI scoreCounterTMP;

        public void Awake()
        {
            scoreCounterTMP = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            highestScoreTMP = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }

        public void Update() => UpdateScoreCount();

        private void UpdateScoreCount()
        {
            int scoreCount = ScoreCounter.Instance.Score;
            int highestScore = PlayerPrefs.GetInt("highscore", 0);

            scoreCounterTMP.text = $"{scoreCount}m";
            if (scoreCount > highestScore)
            {
                highestScoreTMP.text = $"Highest Score : {scoreCount}m";
            }
        }
    }
}