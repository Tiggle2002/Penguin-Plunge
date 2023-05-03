using PenguinPlunge.Pooling;
using PenguinPlunge.Utility;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PenguinPlunge.Core
{
    public class FishGenerator : BaseSpawner
    {
        [SerializeField]
        private Transform parentTransform;

        [TableMatrix(HorizontalTitle = "Fish Layouts", DrawElementMethod = "DrawLayouts",ResizableColumns = false, RowHeight = 16), SerializeField, ListDrawerSettings(NumberOfItemsPerPage = 1)]
        private List<bool[,]> layouts = new List<bool[,]>();

        private ObjectPool<CollectableFish> fishPool;

        private const float StartXDistance = 50f;
        private const float YMin = 17.25f;
        private const float FishLength = 1.4375f;
        private float AdditionX => recentSpawn != null ?  recentSpawn.transform.position.x + FishLength : Player.Instance.transform.position.x + StartXDistance;
        private CollectableFish recentSpawn;

        public void Awake()
        {
            GameObject fishPrefab = Resources.Load<GameObject>("RuntimePrefabs/Collectable/FishPrefab");
            fishPool = new ObjectPool<CollectableFish>(fishPrefab, 100, parentTransform);
            ReflectArrays();
        }

        private void ReflectArrays()
        {
            foreach (var layout in layouts)
            {
                int numRows = layout.GetLength(0);
                int numCols = layout.GetLength(1);

                // Loop through each row and reverse the order of its elements
                for (int i = 0; i < numRows; i++)
                {
                    for (int j = 0; j < numCols / 2; j++)
                    {
                        bool temp = layout[i, j];
                        layout[i, j] = layout[i, numCols - j - 1];
                        layout[i, numCols - j - 1] = temp;
                    }
                }
            }
        }

        [Button("Spawn Specific Layout")]
        private void SpawnSpecificLayout(int index)
        {
            bool[,] layout = layouts[index];

            float startPosX = Player.Instance.transform.position.x + StartXDistance;

            for (int x = 0; x < layout.GetUpperBound(0); x++)
            {
                startPosX += FishLength;
                for (int y = 0; y < layout.GetUpperBound(1)+1; y++)
                {
                    if (IsFishPresentAtCoordinate(x, y))
                    {
                        SpawnFish(startPosX, y);
                    }
                }
            }
            recentSpawn = null;

            bool IsFishPresentAtCoordinate(int x, int y) => layout[x, y];

            void SpawnFish(float posX, float posY)
            {
                recentSpawn = fishPool.Get(new Vector2(posX, YMin + posY));
            }
        }

        [Button("Spawn Layout")]
        public override void Spawn()
        {
            bool[,] layout = layouts.GetRandomElement();
            float startPosX = Player.Instance.transform.position.x + StartXDistance;

            for (int x = 0; x < layout.GetUpperBound(0); x++)
            {
                startPosX += FishLength;
                for (int y = 0; y < layout.GetUpperBound(1); y++)
                {
                    if (IsFishPresentAtCoordinate(x, y))
                    {
                        SpawnFish(startPosX, y);
                    }
                }
            }
            recentSpawn = null;

            bool IsFishPresentAtCoordinate(int x, int y) => layout[x, y];

            void SpawnFish(float posX, float posY)
            {
                recentSpawn = fishPool.Get(new Vector2(posX, YMin + posY));
            }
        }

        public override bool IsFinished() => fishPool.AtCapacity;

        private static bool DrawLayouts(Rect rect, bool value)
        {
            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
            {
                value = !value;
                GUI.changed = true;
                Event.current.Use();
            }
#if UNITY_EDITOR
            EditorGUI.DrawRect(rect.Padding(1), value ? new Color(0.1f, 0.8f, 0.2f) : new Color(0, 0, 0, 0.5f));
#endif
            return value;
        }
    }

}