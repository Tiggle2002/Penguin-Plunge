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
            if (HitByObstacle())
            {
                FSM.TransitionToState(PlayerTransition.Hit);
            }
            if (FSM.Movement.Grounded)
            {
                FSM.TransitionToState(PlayerTransition.Grounded);
            }
        }

        public override void OnEnter() { }

        public override void RunState() 
        {
            PlaySwimAnimation();
        }

        public override void FixedRunState() 
        {
            if (Input.GetKey(KeyCode.Space)) 
            {
                FSM.Movement.ApplyVerticalVelocity(1f);
                FSM.Movement.UpdateGravityScale();
            }
        }

        public override void OnExit() { }

        public override void Initialise() { }

        public override void Dispose() { }

        private void PlaySwimAnimation()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                FSM.Animator.Play("Swim");
            }
            else
            {
                FSM.Animator.Play("Glide");
            }
        }
    }
}