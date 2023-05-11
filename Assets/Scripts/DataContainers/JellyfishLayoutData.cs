using PenguinPlunge.Core;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenguinPlunge.Data
{
    [System.Serializable]
    public class IndividualJellyfishLayout
    {
        public Vector2 Offset => position;
        public float Rotation => rotation;
        public Size Size => size;

        [SerializeField]
        private Vector2 position;
        [SerializeField]
        private float rotation;
        [SerializeField]
        private Size size;

        public IndividualJellyfishLayout(Vector2 position, float rotation, Size size)
        {
            this.position = position;
            this.rotation = rotation;
            this.size = size;
        }
    }
    [System.Serializable]
    public class JellyfishLayout
    {
        public IndividualJellyfishLayout[] Layouts => jellyfishLayouts;

        [SerializeField, ReadOnly]
        private IndividualJellyfishLayout[] jellyfishLayouts;
        [SerializeField, ReadOnly]
        private int pointsRequired;

        public JellyfishLayout(IndividualJellyfishLayout[] jellyfishLayouts, int pointsRequired) 
        {
            this.jellyfishLayouts = jellyfishLayouts;
            this.pointsRequired = pointsRequired;
        }

        public bool EnoughPointsForThisLayout() => ScoreCounter.Instance.Score >= pointsRequired;
    }

    [InlineEditor, CreateAssetMenu(fileName = "JellyfishLayoutData", menuName = "Scriptable Object/Data/Jellyfish Layout Data")]
    public class JellyfishLayoutData : ScriptableObject
    {
        [SerializeField]
        private List<JellyfishLayout> layouts = new();
        private JellyfishLayout currentLayout = null;

        public bool CanSpawnADifferentLayout()
        {
            return layouts.FindAll(layout => layout.EnoughPointsForThisLayout() && layout != currentLayout).Count > 0;
        }

        public IndividualJellyfishLayout[] GetRandomDifferentLayoutAccordingToScore()
        {
            List<JellyfishLayout> potentialLayouts = layouts.FindAll(layout => layout.EnoughPointsForThisLayout() && layout != currentLayout);
            int randomIndex = Random.Range(0, potentialLayouts.Count);
            currentLayout = potentialLayouts[randomIndex];
            return currentLayout.Layouts;
        }

        public void AddLayout(IndividualJellyfishLayout[] jellyfishLayouts, int pointsRequired)
        {
            JellyfishLayout layoutToAdd = new(jellyfishLayouts, pointsRequired);
            layouts.Add(layoutToAdd);
        }
    }
}