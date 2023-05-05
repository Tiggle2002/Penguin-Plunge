using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenguinPlunge.Core
{
    public static class CameraFunctions
    {
        public static Camera Camera 
        {
            get
            {
                if (camera == null)
                {
                    camera = Camera.main;
                }
                return camera;
            }
        }

        public static float ScreenWidth => Camera.orthographicSize * Camera.aspect * 2; 

        private static Camera camera;

        static CameraFunctions()
        {
            camera = Camera.main;
        }

        public static bool VisibleByCamera(this Renderer renderer)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera);
            return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
        }

   
    }
}
