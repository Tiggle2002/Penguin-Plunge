using System;
using System.Collections.Generic;
using UnityEngine;

namespace PenguinPlunge.Visuals
{
    public class Parallax : MonoBehaviour
    {
        #region References
        public List<ParallaxElement> layers;
        #endregion

        #region Private Variables
        private Vector3 speed;
        private Vector3 deltaPosition;
        private Vector3 distanceDifference;
        #endregion

        public void FixedUpdate() => ApplyParallax();

        private void ApplyParallax()
        {
            distanceDifference = transform.position - deltaPosition;

            for (int i = 0; i < layers.Count; i++)
            {
                speed.x = layers[i].horizontalSpeed;
                speed.y = layers[i].verticalSpeed;
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