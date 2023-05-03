using PenguinPlunge.Core;
using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace PenguinPlunge.UI
{
    public class ScoreDisplayer : MonoBehaviour
    {
        [SerializeField, FoldoutGroup("Component References"), Required]
        private TextMeshProUGUI highestScoreTMP;
        [SerializeField, FoldoutGroup("Component References"), Required]
        private TextMeshProUGUI scoreCounterTMP;

        public void Update() => DisplayScoreCount();

        private void DisplayScoreCount()
        {
            highestScoreTMP.DisplayHighScore();
            scoreCounterTMP.DisplayScore();
        }
    }
}