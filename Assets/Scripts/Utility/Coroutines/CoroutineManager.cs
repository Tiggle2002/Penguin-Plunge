using System.Collections;
using UnityEngine;

namespace PenguinPlunge.Utility
{
    public class CoroutineManager : SingletonMonoBehaviour<CoroutineManager>
    {

    }

    public static class CoroutineExtension
    {
        public static Coroutine StartAsCoroutine(this IEnumerator coroutine) => CoroutineManager.Instance.StartCoroutine(coroutine);
    }
}

