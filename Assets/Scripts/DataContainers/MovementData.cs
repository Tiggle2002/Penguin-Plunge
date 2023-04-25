using Sirenix.OdinInspector;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace PenguinPlunge.Data
{
    [InlineEditor, CreateAssetMenu(fileName = "Movement Data", menuName = "Scriptable Object/Data/Movement Data")]
    public class MovementData : ScriptableObject
    {
        #region Speed Variables       
        [SerializeField, Range(0f, 100f), FoldoutGroup("Speed Settings")]
        public float maxSpeed = 4f;
        [SerializeField, Range(0f, 100f), FoldoutGroup("Speed Settings")]
        public float maxAcceleration = 35f;
        #endregion

        #region Jump Variables
        [SerializeField, Range(0f, 100f), FoldoutGroup("Jump Settings")]
        public float downwardGravityScale = 4f;
        [SerializeField, Range(0f, 100f), FoldoutGroup("Jump Settings")]
        public float upwardGravityScale = 1.75f;
        [SerializeField, Range(0.1f, 1f), FoldoutGroup("Jump Settings")]
        public float groundCheckBoxSize = 0.1f;
        [SerializeField, FoldoutGroup("Jump Settings")]
        public float defaultGravityScale = 1f;
        #endregion
    }
}
