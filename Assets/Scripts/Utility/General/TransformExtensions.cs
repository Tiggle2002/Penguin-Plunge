using System.Collections;
using UnityEngine;

namespace PenguinPlunge.Core
{
    public static class TransformExtensions
    {
        public static void TranslateY(this Transform transform, float translation)
        {
            Vector3 translationVector = new(0f, translation, 0f);
            transform.Translate(translationVector);
        }

        public static void SetY(this Transform transform, float newY)
        {
            Vector3 vector = new(transform.position.x, newY, transform.position.z);
            transform.position = vector;
        }

        public static void TranslateX(this Transform transform, float translation)
        {
            Vector3 translationVector = new(translation, 0f, 0f);
            transform.Translate(translationVector);
        }

        public static void SetX(this Transform transform, float newX)
        {
            Vector3 vector = new(newX, transform.position.y, transform.position.z);
            transform.position = vector;
        }

        public static void SetZRotation(this Transform transform, float rotation) => transform.rotation = Quaternion.Euler(0f, 0f, rotation);

        public static bool IsRightToTransform(this Transform fromTransform, Transform toTransform) => XSignOfDirectionFromTransform(fromTransform, toTransform) == 1;
        public static bool IsLeftToTransform(this Transform fromTransform, Transform toTransform) => XSignOfDirectionFromTransform(fromTransform, toTransform) == -1;

        public static float XSignOfDirectionFromTransform(this Transform fromTransform, Transform toTransform) => Mathf.Sign(fromTransform.position.x - toTransform.position.x);
        
        public static float YSignOfDirectionToTransform(this Transform fromTransform, Transform toTransform) => Mathf.Sign(toTransform.position.y - fromTransform.position.y);
    }
}