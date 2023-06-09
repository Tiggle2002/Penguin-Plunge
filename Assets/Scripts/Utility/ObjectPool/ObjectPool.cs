using System;
using System.Collections.Generic;
using UnityEngine;

namespace PenguinPlunge.Pooling
{
    public interface IPool<T>
    {
        T Get(Vector2 position = default);

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
        public bool AtCapacity => objects.Count == poolSize;

        private Stack<T> objects = new();

        private GameObject prefabToPool;
        private Transform parentTransform;
        private int poolSize;

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
            poolSize++;

            return newObj;
        }
        
        public T Get(Vector2 position = default) => GetOrCreate(position);

        private T GetOrCreate(Vector2 position = default)
        {
            if (Count == 0) Create();

            var t = objects.Pop();
            t.gameObject.SetActive(true);
            t.transform.position = position;
            t.OnGet();
            return t;
        }

        public void Return(T t)
        {
            objects.Push(t);
            t.gameObject.SetActive(false);
        }
    }
}
