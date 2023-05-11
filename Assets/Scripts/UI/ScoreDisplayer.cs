using Mono.CompilerServices.SymbolWriter;
using PenguinPlunge.Core;
using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace PenguinPlunge.UI
{
    public class ScoreDisplayer : MonoBehaviour, TEventListener<GameEvent>
    {
        [SerializeField, FoldoutGroup("Component References"), Required]
        private Canvas canvas;
        [SerializeField, FoldoutGroup("Component References"), Required]
        private TextMeshProUGUI highestScoreTMP;
        [SerializeField, FoldoutGroup("Component References"), Required]
        private TextMeshProUGUI scoreCounterTMP;
        [SerializeField, FoldoutGroup("Component References"), Required]
        private TextMeshProUGUI fishCountTMP;

        public void Update() => DisplayScoreCount();

        private void DisplayScoreCount()
        {
            highestScoreTMP.DisplayHighScore();
            scoreCounterTMP.DisplayScore();
            fishCountTMP.DisplayFishCollectedScore();
        }

        public void OnEvent(GameEvent eventData)
        {
            if (eventData.type == GameEventType.GameStart)
            {
                canvas.enabled = true;
            }
            else if (eventData.type == GameEventType.GameOver)
            {
                canvas.enabled = false;
            }
        }

        public void OnEnable() => this.Subscribe();
        public void OnDisable() => this.Unsubscribe();
    }
}