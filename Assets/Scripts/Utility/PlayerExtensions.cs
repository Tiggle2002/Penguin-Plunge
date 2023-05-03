using PenguinPlunge.Core;
using System.Collections;
using UnityEngine;

namespace PenguinPlunge.Utility
{
    public static class PlayerExtensions
    {
        public static bool IsRightOfPlayer(this Transform transform) => transform.IsRightToTransform(Player.Instance.transform);
        public static bool IsLeftOfPlayer(this Transform transform) => transform.IsLeftToTransform(Player.Instance.transform);
        public static float XDirectionFromPlayer(this Transform transform) => transform.XSignOfDirectionFromTransform(Player.Instance.transform);
        public static float YDirectionToPlayer(this Transform transform) => transform.YSignOfDirectionToTransform(Player.Instance.transform);
    }
}