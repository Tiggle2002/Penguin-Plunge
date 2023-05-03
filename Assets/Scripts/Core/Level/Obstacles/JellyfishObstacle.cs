using PenguinPlunge.Data;
using PenguinPlunge.Pooling;
using PenguinPlunge.Utility;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace PenguinPlunge.Core
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class JellyfishObstacle : Obstacle, IPoolable<JellyfishObstacle>
    {
        public Action<JellyfishObstacle> ReturnObject { get; set; }

        public void OnGet() { }

        public void ReturnToPool() => ReturnObject.Invoke(this);

        [SerializeField]
        ObstacleData smallObstacleData;
        [SerializeField]
        ObstacleData mediumObstacleData;
        [SerializeField]
        ObstacleData largeObstacleData;

        ObstacleData currentData;

        private Animator animator;
        private SpriteRenderer sr;
        private BoxCollider2D bc;

        public void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            sr = GetComponentInChildren<SpriteRenderer>();
            bc = GetComponent<BoxCollider2D>();
        }

        public void SetPosition(ObstaclePosition position, Size size)
        {
            if (size == Size.Small) 
            {
                smallObstacleData.PlayAnimation(animator);
                smallObstacleData.SetSprite(sr);
                currentData = smallObstacleData;
            }
            else if (size == Size.Medium) 
            {
                mediumObstacleData.PlayAnimation(animator);
                mediumObstacleData.SetSprite(sr);
                currentData = mediumObstacleData;
            }
            else if (size == Size.Large)
            {
                largeObstacleData.PlayAnimation(animator);
                largeObstacleData.SetSprite(sr);
                currentData = largeObstacleData;
            }
            bc.size = sr.sprite.bounds.size;

            transform.rotation = Quaternion.Euler(0f, 0f, currentData.GetLayoutForPosition(position).GetRotation());
            transform.position = new(transform.position.x, currentData.GetLayoutForPosition(position).GetHeight());
        }

        public IEnumerator DisableOnTime()
        {
            while (this.gameObject.activeInHierarchy) 
            {
                float directionFromPlayer = Mathf.Sign(transform.position.x);
                if (!sr.VisibleByCamera() && directionFromPlayer == -1)
                {
                    ReturnToPool();
                }
                yield return null;
            }
        }
    }
}