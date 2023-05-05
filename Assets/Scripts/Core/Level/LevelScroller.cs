using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PenguinPlunge.Utility;
using System.Linq;

namespace PenguinPlunge.Core
{
    public class LevelScroller : SerializedMonoBehaviour, TEventListener<GameEvent>
    {
        [ShowInInspector, ReadOnly]
        public float CurrentSpeed => currentSpeed;

        [SerializeField, HideLabel, Title("Level Scrolling Speed", TitleAlignment = TitleAlignments.Centered)]
        private float initialSpeed = 0.1f;

        [SerializeField, HideLabel, Title("Level Scrolling Acceleration", TitleAlignment = TitleAlignments.Centered)]
        private float acceleration = 0.1f;

        [SerializeField, HideLabel, Title("Max Scrolling Speed", TitleAlignment = TitleAlignments.Centered)]
        private float maxSpeed = 30f;

        private float currentSpeed;

        public void Awake()
        {
            currentSpeed = initialSpeed;
        }

        private IEnumerator Scroll()
        {
            yield return new WaitForSeconds(0.75f);

            while (true) 
            {
                transform.position += (Vector3)PositionChangeThisFrame();
                currentSpeed += acceleration * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
                yield return null;
            }
        }

        private Vector2 PositionChangeThisFrame()
        {
            float s = currentSpeed * Time.deltaTime;
            return Vector2.left * s;
        }

        private void EndScrollingImmediate() => StopAllCoroutines();

        private IEnumerator EndScrollingOverTime()
        {
            yield return StartCoroutine(CoroutineMethods.ChangeValueOverTimeCoroutine(currentSpeed, 0, 3, s => currentSpeed = s));
            StopAllCoroutines();
        }

        public void OnEvent(GameEvent gameEvent)
        {
            switch (gameEvent.type)
            { 
                case GameEventType.GameStarted:
                    StartCoroutine(Scroll());
                    break;
               case GameEventType.GameOver:
                    EndScrollingImmediate();
                    break;
            }
        }

        public void OnEnable() => this.Subscribe();

        public void OnDisable() => this.Unsubscribe();
    }
}
