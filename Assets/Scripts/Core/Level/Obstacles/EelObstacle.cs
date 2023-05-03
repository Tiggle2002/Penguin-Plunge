using MoreMountains.Feedbacks;
using PenguinPlunge.Utility;
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

        private Animator animator;
        private BoxCollider2D bc;
        private SpriteRenderer sr;

        [SerializeField]
        private MMF_Player eelActiveFeedback;
        [SerializeField]
        private MMF_Player eelSwimFeedback;

        private const float SwimAnimationLength = 2.5f;
        private const float AttackAnimationLength = 2f;
        private const float SwimAwayAnimationLength = 2.5f;

        public void Awake()
        {
            animator = GetComponent<Animator>();
            bc = GetComponent<BoxCollider2D>();
            sr = GetComponent<SpriteRenderer>();
        }

        public void Initiate()
        {
            TriggerAttack().StartAsCoroutine();
            EnableCollisions().StartAsCoroutine();
        }

        private IEnumerator TriggerAttack()
        {
            sr.sprite = warningSprite;
            animator.Play("Swim");
            eelSwimFeedback.PlayFeedbacks();
            yield return new WaitForSeconds(SwimAnimationLength);
            sr.sprite = activeSprite;
            animator.Play("Attack");
            eelActiveFeedback.PlayFeedbacks();
            yield return new WaitForSeconds(AttackAnimationLength);
            sr.sprite = warningSprite;
            animator.Play("SwimAway");
            yield return new WaitForSeconds(SwimAwayAnimationLength);
        }

        private IEnumerator EnableCollisions() 
        {
            bc.enabled = false;
            yield return new WaitForSeconds(SwimAnimationLength);
            bc.enabled = true;
            yield return new WaitForSeconds(AttackAnimationLength); 
            gameObject.SetActive(false);
        }
    }
}