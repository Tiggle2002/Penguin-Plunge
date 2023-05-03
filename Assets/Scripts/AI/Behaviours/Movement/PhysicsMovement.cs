using PenguinPlunge.Core;
using PenguinPlunge.Data;
using System;
using Unity.VisualScripting;
using UnityEngine;

public static class PhysicsMovement
{
    public static void HaltMovement(this Rigidbody2D rb) => rb.velocity = Vector2.zero;

    public static void HaltHorizontalMovementImmediate(this Rigidbody2D rb)
    {
        Vector2 velocity = rb.velocity;
        velocity.x = 0f;
        rb.velocity = velocity;
    }

    public static void HaltMovementImmediate(this Rigidbody2D rb)
    {
        rb.velocity = Vector2.zero;
    }

    public static bool Falling(this Rigidbody2D rb)
    {
        return rb.velocity.y < 0;
    }

    public static void ApplyHorizontalVelocity(this Rigidbody2D rb, float directionX, float acceleration, float maxSpeed)
    {
        Vector2 velocity = rb.velocity;
        
        float maxSpeedChange = acceleration * Time.deltaTime;

        float desiredVeloctiy = directionX * maxSpeed;

        velocity.x = Mathf.MoveTowards(velocity.x, desiredVeloctiy, maxSpeedChange);

        rb.velocity = velocity;
    }

    public static void ApplyVerticalVelocity(this Rigidbody2D rb, float directionY, float acceleration, float maxSpeed)
    {
        Vector2 velocity = rb.velocity;

        float maxSpeedChange = acceleration * Time.deltaTime;

        float desiredVelocity = directionY * maxSpeed;

        velocity.y = Mathf.MoveTowards(velocity.y, desiredVelocity, maxSpeedChange);

        rb.velocity = velocity;
    }

    public static void ApplySinosoidalVerticalVelocity(this Rigidbody2D rb, float directionY, float acceleration, float maxSpeed, float sinWaveFrequency, float sinWaveAmp)
    {
        Vector2 velocity = rb.velocity;

        float maxSpeedChange = acceleration * Time.deltaTime;

        float desiredVelocity = directionY * maxSpeed;

        float sinWave = sinWaveAmp * Mathf.Sin(Time.time * sinWaveFrequency);
        desiredVelocity += sinWave;

        velocity.y = Mathf.MoveTowards(velocity.y, desiredVelocity, maxSpeedChange);

        rb.velocity = velocity;
    }

    public static void PerformJump(this Rigidbody2D rb, float height)
    {
        Vector2 airborneVelocity = rb.velocity;
        airborneVelocity.y += rb.GetJumpSpeed(height);

        rb.velocity = airborneVelocity;
    }

    private static float GetJumpSpeed (this Rigidbody2D rb, float height)
    {
        float verticalSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * height);

        if (rb.velocity.y > 0f)
        {
            verticalSpeed = Mathf.Max(verticalSpeed - rb.velocity.y, 0f);
        }
        else if (rb.velocity.y < 0f)
        {
            verticalSpeed += Mathf.Abs(rb.velocity.y);
        }

        return verticalSpeed;
    }

    public static bool IsFacingRight(this float inputDirection, bool isFacingRight) => (isFacingRight && inputDirection >= 0) || (!isFacingRight && inputDirection > 0);

    public static void AlignScaleToDirection(this Transform transform, bool isFacingRight)
    {
        float scaleX = isFacingRight ? 1f : -1f;
        transform.localRotation = Quaternion.Euler(0, 0, scaleX);
    }

    public static void RotateInDirectionFacing(this Rigidbody2D rb, bool isFacingRight)
    {
        rb.transform.localRotation = rb.GetLocalRotationAccordingToDirection(isFacingRight);
    }

    public static Quaternion GetLocalRotationAccordingToDirection(this Rigidbody2D rb, bool isFacingRight) => Quaternion.Euler(0, isFacingRight ? 0f : -180f, rb.rotation);

    public static void RotateTowardsVelocity(this Rigidbody2D rb)
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        rb.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public static void UpdateGravityScale(this Rigidbody2D rb, IGravity gravity)
    {
        rb.gravityScale = rb.Falling() ? gravity.DownwardScale : gravity.UpwardScale;
    }
}


