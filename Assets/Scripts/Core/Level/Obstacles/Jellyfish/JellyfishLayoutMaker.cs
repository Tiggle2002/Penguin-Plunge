using PenguinPlunge.Core;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenguinPlunge.Data
{
    public class JellyfishLayoutMaker : MonoBehaviour
    {
        [SerializeField, Title("Points Required for Layout to be Spawned", TitleAlignment = TitleAlignments.Centered)]
        private int pointsRequired;

        [SerializeField, Required]
        private JellyfishLayoutData jellyfishDataStructure;
    

        [Button("Add Children To A Fixed Layout")]
        public void SaveCurrentDataToGenerator()
        {
            JellyfishObstacle[] jellyfishChildren = GetComponentsInChildren<JellyfishObstacle>();
            List<IndividualJellyfishLayout> layoutsOfChildren = new();

            foreach (var child in jellyfishChildren)
            {
                IndividualJellyfishLayout childLayout = CreateLayoutFromChild(child);
                layoutsOfChildren.Add(childLayout);
            }
            AddLayoutToLayoutDataStructure();

            IndividualJellyfishLayout CreateLayoutFromChild(JellyfishObstacle child)
            {
                Size size = child.Size;
                Vector2 position = child.transform.position;
                float rotation = child.transform.rotation.eulerAngles.z;

                return new IndividualJellyfishLayout(position, rotation, size);
            }

            void AddLayoutToLayoutDataStructure() => jellyfishDataStructure.AddLayout(layoutsOfChildren.ToArray(), pointsRequired);
        }
    }
}
