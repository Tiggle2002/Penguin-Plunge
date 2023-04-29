using PenguinPlunge.Core;
using System.Collections;
using UnityEngine;

namespace PenguinPlunge.UI
{
    public class LevelFailedCanvas : MonoBehaviour, TEventListener<GameEvent>
    {
        private Canvas canvas;

        public void Awake()
        {
            canvas = GetComponent<Canvas>();
            canvas.enabled = false;
        }

        private void DisplayCanvas()
        {
            canvas.enabled = true;
        }

        public void OnEvent(GameEvent eventData)
        {
            if (eventData.type == GameEventType.GameOver)
            {
                DisplayCanvas();
            }
        }

        public void OnEnable() => this.Subscribe();
        public void OnDisable() => this.Unsubscribe();
    }
}