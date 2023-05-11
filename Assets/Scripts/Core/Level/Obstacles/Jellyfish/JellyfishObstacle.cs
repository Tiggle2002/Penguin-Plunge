using PenguinPlunge.Data;
using PenguinPlunge.Pooling;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace PenguinPlunge.Core
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class JellyfishObstacle : Obstacle, IPoolable<JellyfishObstacle>
    {
        public Action<JellyfishObstacle> ReturnObject { get; set; }

        public void OnGet() { }

        public void ReturnToPool() => ReturnObject.Invoke(this);

        [SerializeField]
        public Size Size;

        [SerializeField]
        ObstacleData smallObstacleData;
        [SerializeField]
        ObstacleData mediumObstacleData;
        [SerializeField]
        ObstacleData largeObstacleData;

        ObstacleData currentData;
        ObstaclePosition currentPosition;


        private Animator animator;
        private SpriteRenderer sr;
        private CapsuleCollider2D capsuleCollider;

        public void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            sr = GetComponentInChildren<SpriteRenderer>();
            capsuleCollider = GetComponent<CapsuleCollider2D>();
        }

        [Button("Set Size")]
        private void SetSize(Size size)
        {
            Awake();
            Size = size;
            SetObstacleDataToMatchSize(size);
            capsuleCollider.size = currentData.Size;
            PlayCurrentDataAnimation();
        }

        public void SetToMatchPosition(ObstaclePosition position, Size size)
        {
            SetObstacleDataToMatchSize(size);
            PlayCurrentDataAnimation();
            capsuleCollider.size = currentData.Size;
            currentPosition = position;
            SetHeightAndRotationToMatchCurrentPosition();
        }

        public void SetToMatchPremadeLayout(Vector2 offset, float rotation, Size size)
        {
            SetObstacleDataToMatchSize(size);
            PlayCurrentDataAnimation();
            capsuleCollider.size = sr.sprite.bounds.size;
            SetPositionAndRotation(offset, rotation);

            void SetPositionAndRotation(Vector2 offset, float rotation)
            {
                transform.position += (Vector3)offset;
                transform.SetZRotation(rotation);
            }
        }

        public void SetObstacleDataToMatchSize(Size size)
        {
            currentData = size switch
            {
                Size.Small => smallObstacleData,
                Size.Medium => mediumObstacleData,
                Size.Large => largeObstacleData,
            };
        }

        public void PlayCurrentDataAnimation()
        {
            currentData.PlayAnimation(animator);
            currentData.SetSprite(sr);
        }

        public void SetHeightAndRotationToMatchCurrentPosition()
        {
            float height = currentData.GetLayoutForPosition(currentPosition).GetHeight();
            float rotation = currentData.GetLayoutForPosition(currentPosition).GetRotation();
            SetHeightAndRotation(height, rotation);
        }

        public void SetHeightAndRotation(float height, float rotation)
        {
            transform.SetY(height);
            transform.SetZRotation(rotation);
        }

        public IEnumerator DisableWhenPassedAndOutOfSight()
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