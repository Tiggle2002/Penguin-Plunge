using System;
using UnityEngine;

namespace PenguinPlunge.Core
{
    public static class TimeScale
    {
        public static bool IsFrozen => isFrozen;

        private static bool isFrozen;

        public static void ToggleFreeze()
        {
            isFrozen = !isFrozen;
            Time.timeScale = isFrozen ? 0 : 1;
        }

        public static void SetFreezeEnabled(bool enabled) 
        {
            isFrozen = enabled;
            Time.timeScale = isFrozen ? 0 : 1;
        }
    }
}
