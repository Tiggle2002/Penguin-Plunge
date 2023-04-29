using PenguinPlunge.Pooling;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenguinPlunge.Core
{
    public abstract class ObstacleSpawner : SerializedMonoBehaviour
    {
        public abstract bool Finished();

        public abstract void Spawn();
     }
}
