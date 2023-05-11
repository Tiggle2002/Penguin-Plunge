using System.Collections;
using UnityEngine;

namespace PenguinPlunge.Utility
{
    public static class CollisionExtensions 
    {
        public static bool InLayer(this Collider2D collider,LayerMask layerMask)
        {
            return ((1 << collider.gameObject.layer) & layerMask) != 0;
        }

        public static bool LayerInLayerMask(int layer, LayerMask layerMask)
        {
            if (((1 << layer) & layerMask) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}