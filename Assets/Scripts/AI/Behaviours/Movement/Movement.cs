using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using PenguinPlunge.Data;

public class Movement : MonoBehaviour
{
    [SerializeField, FoldoutGroup("References")]
    private MovementData movementData;

    #region References
    public Rigidbody2D rb { get; private set; }
    [SerializeField, FoldoutGroup("References")] 
    private BoxCollider2D groundDetector;

    private LayerMask blockLayer;
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
        blockLayer = LayerMask.GetMask("Ground");
    }

    #region Movement Methods
    public bool Grounded => Physics2D.BoxCast(groundDetector.bounds.center, groundDetector.bounds.size, 0f, Vector2.down, movementData.groundCheckBoxSize, blockLayer);

    private void PlayVerticalMovementFeedback()
    {
        if (verticalMovementFeedback == null) return;

        if (!Grounded)
        {
            verticalMovementFeedback.PlayFeedbacks();
            return;
        }
        else if (verticalMovementFeedback.IsPlaying)
        {
            verticalMovementFeedback.StopFeedbacks();
        }
    }

    private void PlayHorizontalMovementFeedback()
    {
        if (horizontalMovementFeedback == null) return;

        if (Grounded)
        {
            horizontalMovementFeedback.PlayFeedbacks();
            return;
        }
        else if (horizontalMovementFeedback.IsPlaying)
        {
            horizontalMovementFeedback.StopFeedbacks();
        }
    }

    public void ApplyVerticalVelocity(float directionY)
    {
        rb.ApplyVerticalVelocity(directionY, movementData.maxAcceleration, movementData.maxSpeed);

        PlayVerticalMovementFeedback();
    }

    public void UpdateGravityScale() => rb.gravityScale = rb.Falling() ? movementData.downwardGravityScale : movementData.upwardGravityScale;

    private IEnumerator PlayFeedbackOnLand()
    {
        groundDetector.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        groundDetector.gameObject.SetActive(true);
        yield return new WaitUntil(() => Grounded);
        landFeedback?.PlayFeedbacks();
    }

    public void PlayLandFeedbacks()
    {
        landFeedback?.PlayFeedbacks();
    }
    #endregion

    #region Physics Checks
    public void OnDrawGizmos()
    {
        if (groundDetector)
        DrawCheck();
    }

    private void DrawCheck()
    {
        Color rayColour = Grounded ? Color.green : Color.red;

        Debug.DrawRay(groundDetector.bounds.center + new Vector3(groundDetector.bounds.extents.x, 0), Vector2.down * (groundDetector.bounds.extents.y + movementData.groundCheckBoxSize), rayColour);
        Debug.DrawRay(groundDetector.bounds.center - new Vector3(groundDetector.bounds.extents.x, 0), Vector2.down * (groundDetector.bounds.extents.y + movementData.groundCheckBoxSize), rayColour);
        Debug.DrawRay(groundDetector.bounds.center - new Vector3(groundDetector.bounds.extents.x, groundDetector.bounds.extents.y + movementData.groundCheckBoxSize), Vector2.right * groundDetector.bounds.extents.x * 2, rayColour);
    }
    #endregion
}


