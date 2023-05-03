using PenguinPlunge.Core;
using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using UnityEngine;

namespace PenguinPlunge.UI
{
    public class CompletionSummary : MonoBehaviour
    {
        [SerializeField, FoldoutGroup("Component References")]
        private TextMeshProUGUI scoreCounterTMP;
        [SerializeField, FoldoutGroup("Component References")]
        private TextMeshProUGUI fishCounterTMP;

        private void DisplayScore()
        {
            scoreCounterTMP.DisplayScore();
            fishCounterTMP.DisplayFishCollectedScore();
        }

        public void Update() => DisplayScore();
    }
}