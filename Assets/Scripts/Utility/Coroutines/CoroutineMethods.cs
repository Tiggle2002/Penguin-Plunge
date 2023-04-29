using Sirenix.OdinInspector.Editor.Validation;
using System;
using System.Collections;
using UnityEngine;

namespace PenguinPlunge.Utility
{
    public static class CoroutineMethods
    {
        public static void AnimateSprite(this SpriteRenderer sr, float interval, params Sprite[] sprites)
        {
            Animate().StartAsCoroutine();

            IEnumerator Animate()
            {
                foreach (var sprite in sprites) 
                {
                    sr.sprite = sprite;
                    yield return new WaitForSeconds(interval);
                }
            }
        }

        public static void EnableForDuration(this Renderer component, float duration)
        {
            EnableComponentForTime().StartAsCoroutine();

            IEnumerator EnableComponentForTime()
            {
                component.enabled = true;
                yield return new WaitForSeconds(duration);
                component.enabled = false;
            }
        }

        public static IEnumerator ChangeValueOverTime(float startValue, float endValue, float duration, Action<float> valueSetter)
        {
            float currentTime = 0;

            while (currentTime < duration) 
            {
                float value = Mathf.Lerp(startValue, endValue, currentTime / duration);
                valueSetter(value);
                currentTime += Time.deltaTime;
                yield return null;
            }

            valueSetter(endValue);
        }

        public static void ExecuteEachFrame(this Action action, float duration, Action onDone = null) => CoroutineManager.Instance.StartCoroutine(PerformEachFrameForTimeCoroutine(action, duration, onDone));

        public static IEnumerator PerformEachFrameForTimeCoroutine(this Action action, float duration, Action onDone = null)
        {
            float currentTime = 0;

            while (currentTime < duration)
            {
                action();
                currentTime += Time.deltaTime;
                yield return null;
            }

            onDone?.Invoke();
        }
    }
}
