using MoreMountains.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenguinPlunge.Utility
{
    public static class AnimationMethods
    {
        public static void LerpSize(this BoxCollider2D bc, Vector2 startSize, Vector2 endSize, float animationLength)
        {
            CoroutineMethods.ChangeOverTime(startSize.x, endSize.x, animationLength, x => bc.size = new(x, bc.size.y));
            CoroutineMethods.ChangeOverTime(startSize.y, endSize.y, animationLength, y => bc.size = new(bc.size.x, y));
        }

        public static void LerpOffSet(this BoxCollider2D bc, Vector2 startOffset, Vector2 endOffset, float animationLength)
        {
            CoroutineMethods.ChangeOverTime(startOffset.x, endOffset.x, animationLength, x => bc.offset = new(x, bc.offset.y));
            CoroutineMethods.ChangeOverTime(startOffset.y, endOffset.y, animationLength, y => bc.offset = new(bc.offset.x, y));
        }

        public static void SetX(this Vector2 v)
        {
            v = new(4, v.y);
        }

        public static void AnimateSprite(this SpriteRenderer sr, float interval, params Sprite[] sprites)
        {
            Animate().StartAsCoroutine();

            IEnumerator Animate()
            {
                foreach (var sprite in sprites)
                {
                    sr.sprite = sprite;
                    yield return new WaitForSeconds(interval);
                }
            }
        }
    }
}
