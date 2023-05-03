using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PenguinPlunge.Visuals
{
    public class Parallax : MonoBehaviour
    {
        #region References
        [SerializeField, TitleGroup("Parallax Objects")]
        private List<ParallaxElement> layers;
        [SerializeField, HorizontalGroup("ParallaxModes")]
        private bool X, Y;
        #endregion

        #region Private Variables
        private Vector3 speed;
        private Vector3 deltaPosition;
        private Vector3 distanceDifference;
        #endregion

        public void Update() => ApplyParallax();

        private void ApplyParallax()
        {
            distanceDifference = transform.position - deltaPosition;

            for (int i = 0; i < layers.Count; i++)
            {
                speed.x = X ? layers[i].horizontalSpeed : 0;
                speed.y = Y ? layers[i].verticalSpeed : 0;
                layers[i].parallaxObject.transform.position += Vector3.Scale(speed, distanceDifference);
            }

            deltaPosition = transform.position;
        }
    }

    [Serializable]
    public struct ParallaxElement
    {
        public GameObject parallaxObject;
        [Range(0f, 0.5f)]
        public float horizontalSpeed;
        [Range(0f, 0.5f)]
        public float verticalSpeed;
    }
}