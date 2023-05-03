using PenguinPlunge.Core;
using TMPro;
using UnityEngine;

namespace PenguinPlunge.UI
{
    public static class UIExtensions
    {
        public static void DisplayFishCollectedScore(this TextMeshProUGUI scoreDisplay)
        {
            scoreDisplay.text = PlayerPrefs.GetInt("fishCount").ToString();
        }

        public static void DisplayScore(this TextMeshProUGUI scoreDisplay)
        {
            int score = ScoreCounter.Instance.Score;
            scoreDisplay.text = $"{score}m";
        }

        public static void DisplayHighScore(this TextMeshProUGUI scoreDisplay)
        {
            int score = ScoreCounter.Instance.HighScore;
            scoreDisplay.text = $"{score}m";
        }
    }
}