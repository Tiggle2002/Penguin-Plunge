using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenguinPlunge.Utility
{
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        GameObject obj = new GameObject($"{typeof(T).Name}");

                        instance = obj.AddComponent<T>();

                        DontDestroyOnLoad(obj);
                    }
                }
                return instance;
            }
        }

        public virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
