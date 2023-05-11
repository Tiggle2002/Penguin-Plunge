using MoreMountains.Feedbacks;
using PenguinPlunge.Utility;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace PenguinPlunge.Core
{
    public class EelObstacle : Obstacle
    {
        [SerializeField]
        private Sprite warningSprite;
        [SerializeField]
        private Sprite activeSprite;

        private Animator eelAnimator;
        private BoxCollider2D eelCollider;

        [SerializeField]
        private MMF_Player eelActiveFeedback;
        [SerializeField]
        private MMF_Player eelSwimFeedback;


        private const float LateStartDelay = 3.5f;
        private const float PresentAnimationLength = 2f;
        private const float EelLifeTimeLength = 5f;

        public void Awake()
        {
            eelAnimator = transform.GetChild(0).GetComponent<Animator>();
            eelCollider = GetComponent<BoxCollider2D>();
        }


        public void ActivateImmediate()
        {
            ActivateImmediate().StartAsCoroutine();

            IEnumerator ActivateImmediate()
            {
                SetPresentState();
                yield return new WaitForSeconds(PresentAnimationLength);
                SetSwimState();
            }
        }

        public void ActivateLate()
        {
            ActivateAfterDelay().StartAsCoroutine();

            IEnumerator ActivateAfterDelay()
            {
                SetPresentState();
                yield return new WaitForSeconds(LateStartDelay);
                Mathf.Clamp(LateStartDelay, 3f, 3.5f);
                ActivateImmediate();
            }
        }

        [Button("Debug Present State")]
        private void SetPresentState()
        {
            eelAnimator.Play("Idle");
        }

        [Button("Debug Swim State")]
        private void SetSwimState()
        {
            eelAnimator.Play("Swim", -1, 0);
            if (!eelSwimFeedback.IsPlaying)
            {
                eelSwimFeedback.PlayFeedbacks();
            }
            eelCollider.EnableForDuration(EelLifeTimeLength);
            MoveColliderWithEel().StartAsCoroutine();
            gameObject.Disable(EelLifeTimeLength);

             IEnumerator MoveColliderWithEel()
            {
                eelCollider.LerpOffSet(new Vector2(17, 0), new Vector2(0, 0), 2.5f);
                eelCollider.LerpSize(new Vector2(6, 2), new Vector2(40, 2), 2.5f);
                yield return new WaitForSeconds(2.5f);
                eelCollider.LerpOffSet(new Vector2(0, 0), new Vector2(-19.5f, 0), 2.5f);
                eelCollider.LerpSize(new Vector2(40, 2), new Vector2(1, 2), 2.5f);
            }
        }
    }
}