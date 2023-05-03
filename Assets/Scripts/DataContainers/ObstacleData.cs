using PenguinPlunge.Core;
using PenguinPlunge.Utility;
using Sirenix.OdinInspector;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace PenguinPlunge.Data
{
    [InlineEditor, CreateAssetMenu(fileName = "ObstacleData", menuName = "Scriptable Object/Data/Obstacle Data")]
    public class ObstacleData : SerializedScriptableObject
    {
        [SerializeField, EnumToggleButtons]
        private Size obstacleSize;

        [SerializeField]
        private Sprite sprite;

        [SerializeField]
        private string animationName;

        [SerializeField]
        private ObstacleLayout[] layouts = new ObstacleLayout[] { new ObstacleLayout(ObstaclePosition.Top), new ObstacleLayout(ObstaclePosition.Middle), new ObstacleLayout(ObstaclePosition.Bottom) };

        public ObstacleLayout GetLayoutForPosition(ObstaclePosition position) => layouts.First(layout => layout.MatchesPosition(position));

        public void SetSprite(SpriteRenderer sr)
        {
            sr.sprite = sprite; 
        }

        public void PlayAnimation(Animator animator)
        {
            animator.Play(animationName);
        }
    }

    [System.Serializable]
    public class ObstacleLayout
    {
        public ObstaclePosition Position => position;

        [SerializeField, EnumToggleButtons, TitleGroup("Values for Position", Alignment = TitleAlignments.Centered), HideLabel]
        private ObstaclePosition position;
        [SerializeField, HorizontalGroup("Values")]
        private Vector2Int height;
        [SerializeField, HorizontalGroup("Values")]
        private int[] availableRotations;

        public ObstacleLayout(ObstaclePosition position)
        {
            this.position = position;
        }

        public bool MatchesPosition(ObstaclePosition position) => this.position == position;
        public int GetHeight() => height.RandomInRange();
        public int GetRotation() => availableRotations.GetRandomElement();
    }

    public enum ObstaclePosition { Top, Middle, Bottom }

    public enum Size { Small, Medium, Large }
}