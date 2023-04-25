using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using PenguinPlunge.Pooling;

namespace PenguinPlunge.Tests
{
    public class PoolObjectTest
    {
        private ObjectPool<TestPoolObject> testPool;
        private GameObject testPrefab;

        [SetUp]
        public void Setup()
        {
            testPrefab = new GameObject();
            testPrefab.AddComponent<TestPoolObject>();
        }

        [Test]
        public void ReturnAction_ReturnsObject()
        {
            testPool = new(testPrefab, 1, null);

            var obj = testPool.Get();
            obj.ReturnObject(obj);

            int expectedCount = 1;
            int actualCount = testPool.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
