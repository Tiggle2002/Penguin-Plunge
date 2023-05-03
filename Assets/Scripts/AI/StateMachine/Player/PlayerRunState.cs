using PenguinPlunge.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenguinPlunge.AI
{
    public class PlayerRunState : PlayerFSMState
    {
        private float animatorSpeed = 1f;

        public PlayerRunState() : base()
        {
            stateID = PlayerStateID.Run;
        }

        public override void EvaluateState() 
        {
            if (HitByObstacle())
            {
                FSM.TransitionToState(PlayerTransition.Hit);
            }
#if UNITY_ANDROID || UNITY_EDITOR_IOS
            if (Input.touchCount > 0)
            {
                FSM.TransitionToState(PlayerTransition.JumpPressed);
            }
#endif
#if UNITY_EDITOR || UNITY_WEBGL || UNITY_STANDALONE
            if (Input.GetKey(KeyCode.Space))
            {
                FSM.TransitionToState(PlayerTransition.JumpPressed);
            }
#endif
        }

        public override void OnEnter() => FSM.Animator.Play("Run");

        public override void RunState()
        {
            IncreaseFeedbackIntensityWithRespectToScore();
            FSM.Movement.PlayHorizontalMovementFeedback();
        }

        public override void FixedRunState() { }

        public override void OnExit() => FSM.Animator.speed = 1f;

        public override void Initialise() { }

        public override void Dispose() { }

        private int previousScore;
        public void IncreaseFeedbackIntensityWithRespectToScore()
        {
            if (previousScore == ScoreCounter.Instance.Score || ScoreCounter.Instance.Score == 0) return;
            previousScore = ScoreCounter.Instance.Score;
            if (previousScore % 50 == 0)
            {
                animatorSpeed += 0.05f;
                FSM.Animator.speed = animatorSpeed;
            }
        }
    }
}
