using System;
using System.Collections.Generic;
using UnityEngine;

namespace PenguinPlunge.Pooling
{
    public interface IPool<T>
    {
        T Get();

        void Return(T t);
    }

    public interface IPoolable<T>
    {
        Action<T> ReturnObject { get; set; }

        void OnGet();
    }

    public class ObjectPool<T> : IPool<T> where T : MonoBehaviour, IPoolable<T>
    {
        public int Count => objects.Count;

        private Stack<T> objects = new();

        private GameObject prefabToPool;
        private Transform parentTransform;


        public ObjectPool(GameObject prefabToPool, int poolSize, Transform parentTransform = null)
        {
            this.prefabToPool = prefabToPool;
            this.parentTransform = parentTransform;

            for (int i = 0; i < poolSize; i++)
            {
                Create();
            }
        }

        private T Create()
        {
            var newObj = GameObject.Instantiate(prefabToPool, parentTransform).GetComponent<T>();
            newObj.gameObject.SetActive(false);
            newObj.ReturnObject = Return;
            objects.Push(newObj);

            return newObj;
        }
        
        public T Get() => GetOrCreate();

        public T GetOrCreate()
        {
            if (Count == 0)
            {
                return Create();
            }

            var t = objects.Pop();
            t.gameObject.SetActive(true);
            t.OnGet();
            return t;
        }

        public GameObject PullGameObject()
        {
            return Get().gameObject;
        }

        public void Return(T t)
        {
            objects.Push(t);
            t.gameObject.SetActive(false);
        }
    }
}
