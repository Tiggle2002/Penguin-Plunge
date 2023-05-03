using PenguinPlunge.Pooling;
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

        public static void ExecuteAtEndOfEachFrame(this Action action, float duration, Action onDone = null) => CoroutineManager.Instance.StartCoroutine(PerformAtTheEndOfEachFrameForTimeCoroutine(action, duration, onDone));

        public static IEnumerator PerformAtTheEndOfEachFrameForTimeCoroutine(this Action action, float duration, Action onDone = null)
        {
            float currentTime = 0;

            while (currentTime < duration)
            {
                action();
                currentTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            onDone?.Invoke();
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

        public static void ReturnToPoolOnCondition<T>(this T objectToDisable, Func<bool> condition) where T : IPoolable<T>
        {
            DisableOnCondition().StartAsCoroutine();

            IEnumerator DisableOnCondition()
            {
                yield return new WaitUntil(() => condition());
                objectToDisable.ReturnObject(objectToDisable);
            }
        }

        public static void StartWithDelay(this IEnumerator coroutine, float delay)
        {
            StartWithDelay().StartAsCoroutine();

            IEnumerator StartWithDelay()
            {
                yield return new WaitForSeconds(delay);
                coroutine.StartAsCoroutine();
            }
        }
    }
}
