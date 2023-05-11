using PenguinPlunge.Utility;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace PenguinPlunge.Core
{
    public class EelObstacleSpawner : BaseSpawner
    {
        [TableMatrix(HorizontalTitle = "Eel Layouts", DrawElementMethod = "DrawTestLayout", ResizableColumns = false, RowHeight = 16), SerializeField]
        private int[,] layouts = new int[4, 6];

        private EelObstacle[] eelObstacles;

        private bool active;

        public void Awake()
        {
            eelObstacles = GetComponentsInChildren<EelObstacle>(true);
            eelObstacles.Sort(SortByHeight);
        }

        public override void Spawn() => ActivateLayout();

        public override bool IsFinished() => !active;

        [Button("New Random Layout")]
        private void ActivateLayout()
        {
            int[] layout = GetRandomLayout();

            for (int i = 0; i < layout.Length; i++)
            {
                if (layout[i] == 0) //None
                {
                    continue;
                }

                eelObstacles[i].gameObject.SetActive(true);
                if (layout[i] == 1) //Early
                {
                    eelObstacles[i].ActivateImmediate();
                }
                if (layout[i] == 2) //Late
                {
                    eelObstacles[i].ActivateLate();
                }
            }
            SetFinished().StartAsCoroutine();

             IEnumerator SetFinished()
            {
                active = true;
                yield return new WaitForSeconds(12.5f);
                active = false;
            }
        }

        private int[] GetRandomLayout()
        {
            int randomIndex = Random.Range(0, layouts.GetLength(0));
            int[] layout = new int[layouts.GetLength(1)];

            for (int i = 0; i < layout.Length; i++)
            {
                layout[i] = layouts[randomIndex, i];
            }
            return layout;
        }

        private int SortByHeight(MonoBehaviour transformA, MonoBehaviour transformB)
        {
            if (transformA.transform.position.y > transformB.transform.position.y) 
            {
                return 1;
            }
            else if (transformA.transform.position.y < transformB.transform.position.y)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        private static int DrawTestLayout(Rect rect, int value)
        {
            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
            {
                value++;
                value %= 3;
                GUI.changed = true;
                Event.current.Use();
            }

#if UNITY_EDITOR
            EditorGUI.DrawRect(rect.Padding(1), GetSquareColor());
#endif
            return value;

            Color GetSquareColor()
            {
                return value switch
                {
                    0 => new Color(0, 0, 0, 0.5f), //None
                    1 => new Color(1, 0, 0, 1f), //Red
                    2 => new Color(0.1f, 0.8f, 0.2f) //Green
                };
            }
        }

        private static bool DrawLayouts(Rect rect, bool value)
        {
            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))     
            {
                value = !value;
                GUI.changed = true;
                Event.current.Use();
            }
#if UNITY_EDITOR
            EditorGUI.DrawRect(rect.Padding(1), value ? new Color(0.1f, 0.8f, 0.2f)  : new Color(0, 0,0, 0.5f));
#endif
            return value;
        }
    }

}