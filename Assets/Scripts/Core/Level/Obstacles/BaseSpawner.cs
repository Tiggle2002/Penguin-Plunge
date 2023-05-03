using PenguinPlunge.Pooling;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenguinPlunge.Core
{
    public abstract class BaseSpawner : SerializedMonoBehaviour
    {
        public abstract bool IsFinished();

        public abstract void Spawn();
     }
}
