using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenguinPlunge.AI
{
    public class PlayerRunState : PlayerFSMState
    {
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
            if (Input.GetKey(KeyCode.Space))
            {
                FSM.TransitionToState(PlayerTransition.JumpPressed);
            }
        }

        public override void OnEnter() 
        {
            FSM.Animator.Play("Run");
        }

        public override void RunState() 
        {
        }

        public override void FixedRunState() { }

        public override void OnExit() { }

        public override void Initialise() { }

        public override void Dispose() { }
    }
}
