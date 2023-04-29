using PenguinPlunge.Core;
using System.Collections;
using UnityEngine;

namespace PenguinPlunge.AI
{
    public class PlayerDeathState : PlayerFSMState
    {
        public PlayerDeathState() : base() 
        {
            stateID = PlayerStateID.Death;
        }

        public override void EvaluateState() { }

        public override void OnEnter() 
        {
            GameEvent.Trigger(GameEventType.GameOver);
        }

        public override void RunState() 
        {
            PlayDeathAnimation();
        }

        public override void FixedRunState() { }

        public override void OnExit() { }

        public override void Initialise() { }

        public override void Dispose() { }

        private void PlayDeathAnimation()
        {
            if (Mathf.Approximately(FSM.Rigidbody.velocity.y, 0))
            {
                FSM.Animator.Play("Death");
            }
            else
            {
                FSM.Animator.Play("Fall");
            }
        }
    }
}