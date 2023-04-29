using PenguinPlunge.Utility;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace PenguinPlunge.Core
{
    public class EelObstacleSpawner : ObstacleSpawner
    {
        [TableMatrix(HorizontalTitle = "Eel Layouts", DrawElementMethod = "DrawLayouts", ResizableColumns = false, RowHeight = 16), SerializeField]
        private bool[,] layouts = new bool[4, 6];

        [SerializeField]
        private EelObstacle[] eelObstacles;

        private bool active;

        public void Awake()
        {
            eelObstacles.Sort(SortByHeight);
        }

        public override void Spawn() => ActivateALayout();

        public override bool Finished() => !active;

        [Button("RandomLayout")]
        private void ActivateALayout()
        {
            bool[] layout = GetRandomLayout();

            for (int i = 0; i < layout.Length; i++)
            {
                eelObstacles[i].gameObject.SetActive(layout[i]);
                if (layout[i])
                {
                    eelObstacles[i].Initiate();
                }
            }
            SetFinished().StartAsCoroutine();
        }

        private IEnumerator SetFinished()
        {
            active = true;
            yield return new WaitForSeconds(7.5f);
            active = false; 
        }

        private bool[] GetRandomLayout()
        {
            int randomIndex = Random.Range(0, layouts.GetLength(0));
            bool[] layout = new bool[layouts.GetLength(1)];

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

        private static bool DrawLayouts(Rect rect, bool value)
        {
            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))     
            {
                value = !value;
                GUI.changed = true;
                Event.current.Use();
            }

            UnityEditor.EditorGUI.DrawRect(rect.Padding(1), value ? new Color(0.1f, 0.8f, 0.2f)  : new Color(0, 0,0, 0.5f));

            return value;
        }
    }
}