﻿using System.Collections;
using UnityEngine;
using System;

namespace PenguinPlunge.Pooling
{
    public class PoolObject : MonoBehaviour, IPoolable<PoolObject>
    {
        public Action<PoolObject> ReturnObject { get; set; }

        public void OnGet() { }

        private void ReturnToPool() => ReturnObject?.Invoke(this);
    }
}
