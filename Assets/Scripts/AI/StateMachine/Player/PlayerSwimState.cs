using PenguinPlunge.Core;
using System;
using System.Collections;
using UnityEngine;

namespace PenguinPlunge.AI
{
    public class PlayerSwimState : PlayerFSMState
    {
        public PlayerSwimState() : base()
        {
            stateID = PlayerStateID.Swim;
        }

        public override void EvaluateState() 
        {
            if (!PlayerInput.InputEnabled) return;

            if (HitByObstacle())
            {
                FSM.TransitionToState(PlayerTransition.Hit);
            }
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            if (FSM.Movement.IsGrounded && !Input.GetKey(KeyCode.Space))
            {
                FSM.TransitionToState(PlayerTransition.Grounded);
            }
#endif
#if UNITY_IOS || UNITY_ANDROID
            if (FSM.Movement.IsGrounded && Input.touchCount == 0)
            {
                FSM.TransitionToState(PlayerTransition.Grounded);
            }
#endif
        }

        public override void OnEnter() { }

        public override void RunState() 
        {
            base.RunState();
            PlaySwimAnimation();
            FSM.Movement.PlayVerticalMovementFeedback();
        }

        public override void FixedRunState() 
        {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            if (Input.GetKey(KeyCode.Space))
            {
                FSM.Movement.ApplyVerticalVelocityWithFeedback(1f);
            }
#endif
#if UNITY_IOS || UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                FSM.Movement.ApplyVerticalVelocityWithFeedback(1f);
            }
#endif
        }

        public override void OnExit() { }

        public override void Initialise() { }

        public override void Dispose() { }

        private void PlaySwimAnimation()
        {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            if (Input.GetKey(KeyCode.Space))
            {
                FSM.Animator.Play("Swim");
            }
#endif
#if UNITY_IOS || UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                FSM.Animator.Play("Swim");
            }
#endif
            else
            {
                FSM.Animator.Play("Glide");
            }
        }
    }
}