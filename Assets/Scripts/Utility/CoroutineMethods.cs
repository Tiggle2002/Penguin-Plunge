using Sirenix.OdinInspector.Editor.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenguinPlunge.Utility
{
    public static class CoroutineMethods
    {
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
    }
}
