using PenguinPlunge.Pooling;
using System;
using System.Collections;
using UnityEngine;

namespace PenguinPlunge.Utility
{
    public static class CoroutineMethods
    {
        public static void Disable(this GameObject gameObject, float delay)
        {
            Disable().StartAsCoroutine();
            IEnumerator Disable()
            {
                yield return new WaitForSeconds(delay);
                gameObject.SetActive(false);
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

        public static void EnableForDuration(this Collider2D component, float duration)
        {
            EnableComponentForTime().StartAsCoroutine();

            IEnumerator EnableComponentForTime()
            {
                component.enabled = true;
                yield return new WaitForSeconds(duration);
                component.enabled = false;
            }
        }

        public static void ChangeOverTime(float startValue, float endValue, float duration, Action<float> valueSetter)
        {
            ChangeValueOverTimeCoroutine(startValue, endValue, duration, valueSetter).StartAsCoroutine();

            IEnumerator ChangeValueOverTimeCoroutine(float startValue, float endValue, float duration, Action<float> valueSetter)
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
        }

        public static void ExecuteAtEndOfEachFrame(this Action action, float duration, Action onDone = null)
        {
            CoroutineManager.Instance.StartCoroutine(PerformAtTheEndOfEachFrameForTimeCoroutine(action, duration, onDone));

             IEnumerator PerformAtTheEndOfEachFrameForTimeCoroutine(Action action, float duration, Action onDone = null)
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
    }


        public static void ExecuteEachFrame(this Action action, float duration, Action onDone = null)
        {
            CoroutineManager.Instance.StartCoroutine(PerformEachFrameForTimeCoroutine(action, duration, onDone));

            IEnumerator PerformEachFrameForTimeCoroutine(Action action, float duration, Action onDone = null)
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
