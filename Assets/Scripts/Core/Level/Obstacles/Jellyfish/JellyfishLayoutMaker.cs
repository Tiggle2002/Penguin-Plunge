using PenguinPlunge.Core;
using PenguinPlunge.Data;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishLayoutMaker : MonoBehaviour
{
    [Button("Add Children To A Fixed Layout")]
    public void SaveCurrentDataToGenerator()
    {
        JellyfishObstacle[] jellyfishChildren = GetComponentsInChildren<JellyfishObstacle>();

        List<ObjectLayout> layoutsOfChildren = new();

        foreach (var child in jellyfishChildren)
        {
            Size size = child.Size;
            Vector2 position = child.transform.position;
            float rotation = child.transform.rotation.eulerAngles.z;

            ObjectLayout childLayout = new(position, rotation, size);
            layoutsOfChildren.Add(childLayout);
        }
        JellyfishSpawner jellyfishSpawner = FindObjectOfType<JellyfishSpawner>();
        jellyfishSpawner.AddPremadeLayout(layoutsOfChildren.ToArray());
    }
}
