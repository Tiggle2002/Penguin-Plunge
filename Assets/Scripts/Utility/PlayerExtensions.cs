using PenguinPlunge.Core;
using System.Collections;
using UnityEngine;

public static class PlayerExtensions 
{
    public static bool IsRightOfPlayer(this Transform transform) => transform.IsRightToTransform(Player.Instance.transform);
    public static bool IsLeftOfPlayer(this Transform transform) => transform.IsLeftToTransform(Player.Instance.transform);
    public static float XDirectionFromPlayer(this Transform transform) => transform.SignOfDirectionFromTransform(Player.Instance.transform);
}