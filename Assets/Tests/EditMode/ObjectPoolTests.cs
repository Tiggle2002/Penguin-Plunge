using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using PenguinPlunge.Pooling;
using UnityEngine;
using UnityEngine.TestTools;

namespace PenguinPlunge.Tests
{
    public class ObjectPoolTests
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
        public void Get_ReturnsObjectReference()
        {
            testPool = new(testPrefab, 1, null);

            var testObject = testPool.Get();

            Assert.IsNotNull(testObject);
        }

        [Test]
        public void Get_DecreasesCount()
        {
            testPool = new(testPrefab, 1, null);

            testPool.Get();
            int expectedCount = 0;
            int actualCount = testPool.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void Get_CreatesNewIfCountIsZero()
        {
            testPool = new(testPrefab, 0, null);

            var obj = testPool.Get();

            Assert.IsNotNull(obj);
        }

        [Test]
        public void Return_ReturnsObject()
        {
            testPool = new(testPrefab, 1, null);

            var obj = testPool.Get();
            testPool.Return(obj);
            int expectedCount = 1;
            int actualCount = testPool.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void Get_ReusesObjects()
        {
            testPool = new (testPrefab, 1, null);

            var obj1 = testPool.Get();
            testPool.Return(obj1);
            var obj2 = testPool.Get();

            Assert.AreEqual(obj1, obj2);
            }
    }

    public class TestPoolObject : MonoBehaviour, IPoolable<TestPoolObject>
    {
        public Action<TestPoolObject> ReturnObject { get; set; }

        public void OnGet() { }
    }
 }