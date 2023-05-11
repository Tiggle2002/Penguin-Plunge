using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using PenguinPlunge.Data;
using System;
using PenguinPlunge.Utility;

namespace PenguinPlunge.Core
{
    public interface IGravity
    {
        float UpwardScale { get; }
        float DownwardScale { get; }
        void SetScale(float newScale, float duration);
    }

    public sealed class Movement : MonoBehaviour, IGravity
    {
        [SerializeField, FoldoutGroup("References")]
        private MovementData movementData;

        #region References
        public Rigidbody2D rb { get; private set; }
        [SerializeField, FoldoutGroup("References")]
        private BoxCollider2D groundDetector;

        private LayerMask blockLayer;
        private float lastJumpTime;
        #endregion

        #region Feedbacks
        [FoldoutGroup("Feedback"), SerializeField]
        private MMF_Player verticalMovementFeedback;
        [FoldoutGroup("Feedback"), SerializeField]
        private MMF_Player horizontalMovementFeedback;
        [FoldoutGroup("Feedback"), SerializeField]
        private MMF_Player landFeedback;
        #endregion

        public void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            blockLayer = LayerMask.GetMask("Ground");
        }

        public void Update()
        {
            IncreaseFeedbackIntensityWithRespectToScore();
            rb.UpdateGravityScale(this);
        }

        #region Movement Methods
        public bool IsGrounded => Physics2D.BoxCast(groundDetector.bounds.center, groundDetector.bounds.size, 0f, Vector2.down, movementData.groundCheckBoxSize, blockLayer);

        public float UpwardScale => movementData.upwardGravityScale;

        public float DownwardScale => movementData.downwardGravityScale;

        public void PlayVerticalMovementFeedback()
        {
            if (verticalMovementFeedback == null) return;

#if UNITY_EDITOR || UNITY_WEBGL || UNITY_STANDALONE
            if (!IsGrounded && Input.GetKey(KeyCode.Space))
            {
                verticalMovementFeedback.PlayFeedbacks();
            }
#endif
#if UNITY_IOS || UNITY_ANDROID
            if (!IsGrounded && Input.touchCount > 0)
            {
                verticalMovementFeedback.PlayFeedbacks();
            }
#endif
            else
            {
                verticalMovementFeedback.StopFeedbacks();
            }
        }

        public void PlayHorizontalMovementFeedback()
        {
            if (horizontalMovementFeedback == null) return;

            if (IsGrounded)
            {
                horizontalMovementFeedback.PlayFeedbacks();
            }
            else if (horizontalMovementFeedback.IsPlaying)
            {
                horizontalMovementFeedback.StopFeedbacks();
            }
        }

        public void ApplyVerticalVelocity(float directionY)
        {
            rb.ApplyVerticalVelocity(directionY, movementData.maxAcceleration, movementData.maxSpeed);
        }

        public void ApplyVerticalVelocityWithFeedback(float directionY)
        {
            rb.ApplyVerticalVelocity(directionY, movementData.maxAcceleration, movementData.maxSpeed);

            PlayVerticalMovementFeedback();
            PlayFeedbackOnLand().StartAsCoroutine();
        }

        private IEnumerator PlayFeedbackOnLand()
        {
            yield return new WaitForSeconds(0.5f);
            if (IsGrounded)
            {
                yield break;
            }
            yield return new WaitUntil(() => IsGrounded);
            landFeedback?.PlayFeedbacks();
        }

        public void PlayLandFeedbacks()
        {
            landFeedback?.PlayFeedbacks();
        }

        private int previousScore;
        public void IncreaseFeedbackIntensityWithRespectToScore()
        {
            if (previousScore == ScoreCounter.Instance.Score || ScoreCounter.Instance.Score == 0) return;
            previousScore = ScoreCounter.Instance.Score;
            if (previousScore % 100 == 0)
            {
                horizontalMovementFeedback.CooldownDuration -= 0.035f;
                horizontalMovementFeedback.CooldownDuration = Mathf.Clamp(horizontalMovementFeedback.CooldownDuration, 0.15f, 1f);
            }
        }
        #endregion

        #region Physics Checks
        public void OnDrawGizmos()
        {
            if (groundDetector)
            {
                DrawGroundCheck();
            }
        }

        private void DrawGroundCheck()
        {
            Color rayColour = IsGrounded ? Color.green : Color.red;

            Debug.DrawRay(groundDetector.bounds.center + new Vector3(groundDetector.bounds.extents.x, 0), Vector2.down * (groundDetector.bounds.extents.y + movementData.groundCheckBoxSize), rayColour);
            Debug.DrawRay(groundDetector.bounds.center - new Vector3(groundDetector.bounds.extents.x, 0), Vector2.down * (groundDetector.bounds.extents.y + movementData.groundCheckBoxSize), rayColour);
            Debug.DrawRay(groundDetector.bounds.center - new Vector3(groundDetector.bounds.extents.x, groundDetector.bounds.extents.y + movementData.groundCheckBoxSize), Vector2.right * groundDetector.bounds.extents.x * 2, rayColour);
        }

        public void SetScale(float newScale, float duration = 0)
        {
            if (duration > 0)
            {
                CoroutineMethods.ExecuteAtEndOfEachFrame(() => rb.gravityScale = newScale, duration, () => rb.gravityScale = movementData.defaultGravityScale);
            }
            else
            {
                rb.gravityScale = newScale;
            }
        }
        #endregion
    }
}


