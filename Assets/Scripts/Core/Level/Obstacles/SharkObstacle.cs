using PenguinPlunge.Pooling;
using System;
using System.Collections;
using UnityEngine;
using PenguinPlunge.Utility;
using Sirenix.OdinInspector;
using MoreMountains.Feedbacks;

namespace PenguinPlunge.Core
{
    public class SharkObstacle : Obstacle, IPoolable<SharkObstacle>
    {
        public Action<SharkObstacle> ReturnObject { get; set; }

        private float YDirectionToPlayer => Mathf.Sign(Player.Instance.transform.position.y - transform.position.y);

        [SerializeField]
        private GameObject sharkObj;

        [SerializeField, FoldoutGroup("Sprite References")]
        private SpriteRenderer warningSpriteRenderer;
        [SerializeField, FoldoutGroup("Sprite References")]
        private SpriteRenderer sharkSpriteRender;

        [SerializeField, FoldoutGroup("Sprite References")]
        private Sprite sharkWarningSprite;
        [SerializeField, FoldoutGroup("Sprite References")]
        private Sprite sharkIncomingSprite;


        [SerializeField, FoldoutGroup("Shark Feedback")]
        private MMF_Player sharkIncomingFeedback;
        [SerializeField, FoldoutGroup("Shark Feedback")]
        private MMF_Player sharkSwimFeedback;

        private const float WarningDuration = 2.5f;
        private const float  FinalWarningDuration = 0.5f;
        private float horizontalSpeed;
        private float verticalSpeed;

        public void SetSpeed(float horizontalSpeed, float verticalSpeed)
        {
            this.horizontalSpeed = horizontalSpeed;
            this.verticalSpeed = verticalSpeed;
        }

        public void OnGet()
        {
            sharkSwimFeedback.StopFeedbacks();
            SetSharkEnabled(false);
            PlaceAtTopRight();
            PlaySharkWarningSequence();
            PlayFeedback();
            CoroutineMethods.ExecuteEachFrame(AlignPositionToPlayerHeight, 2.5f, BeginSharkLaunch);
        }

        private void SetSharkEnabled(bool enabled) => sharkObj.SetActive(enabled);

        private void PlaceAtTopRight()
        {
            float positionX = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - sharkWarningSprite.bounds.size.x;
            float positionY = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y;
            transform.position = new Vector3(positionX, positionY);
        }

        private void PlaySharkWarningSequence()
        {
            warningSpriteRenderer.EnableForDuration(WarningDuration + FinalWarningDuration);
            warningSpriteRenderer.AnimateSprite(2.5f, sharkWarningSprite, sharkIncomingSprite);
        }

        private void PlayFeedback()
        {
            sharkIncomingFeedback.InitialDelay = 2.5f;
            sharkIncomingFeedback.PlayFeedbacks();
        }

        private void AlignPositionToPlayerHeight() => transform.TranslateY(GetVerticalMovement());

        private float GetVerticalMovement() => YDirectionToPlayer * verticalSpeed * Time.deltaTime;

        private void BeginSharkLaunch() => MoveSharkAfterFinalWarning().StartAsCoroutine();

        private IEnumerator MoveSharkAfterFinalWarning()
        {
            yield return new WaitForSeconds(FinalWarningDuration);

            SetSharkEnabled(true);
            SetSharkInitialPositionAndPlayFeedback();
            while (warningSpriteRenderer.VisibleByCamera() || transform.IsRightOfPlayer())
            {
                transform.TranslateX(GetMovementX());
                yield return new WaitForFixedUpdate();
            }
            ReturnToSharkPoolAndStopFeedback();
        }

        private float GetMovementX() => -1 * horizontalSpeed * Time.deltaTime * 15;

        private void SetSharkInitialPositionAndPlayFeedback()
        {
            sharkSwimFeedback.PlayFeedbacks();
            transform.SetX(Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x + sharkWarningSprite.bounds.size.x);
        }

        private void ReturnToSharkPoolAndStopFeedback()
        {
            sharkSwimFeedback.StopFeedbacks();
            ReturnObject(this);
        }
    }
}