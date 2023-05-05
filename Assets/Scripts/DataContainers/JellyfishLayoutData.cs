using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace PenguinPlunge.Data
{
    public class ObjectLayout
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

        public ObjectLayout(Vector2 position, float rotation, Size size)
        {
            this.position = position;
            this.rotation = rotation;
            this.size = size;
        }
    }

    [InlineEditor, CreateAssetMenu(fileName = "JellyfishLayoutData", menuName = "Scriptable Object/Data/Jellyfish Layout Data")]
    public class JellyfishLayoutData : ScriptableObject
    {

    }
}