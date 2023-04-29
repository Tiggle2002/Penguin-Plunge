using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using PenguinPlunge.Utility;

namespace PenguinPlunge.Tests
{
    public class CoroutineMethodsTests
    {
        [UnityTest]
        public IEnumerator ChangeValueOverTime_ValueCorrect()
        {
            float startValue = 0;
            float endValue = 5;
            float duration = 1;

            float value = startValue;
            yield return CoroutineMethods.ChangeValueOverTime(startValue, endValue, duration, v => value = v);
           
            Assert.AreEqual(endValue, value);
        }
    }
}
