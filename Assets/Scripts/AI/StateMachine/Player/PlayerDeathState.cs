using PenguinPlunge.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

namespace PenguinPlunge.AI
{
    public class PlayerDeathState : PlayerFSMState
    {
        private bool playShockAnimation = false;
        private float shockTimeRemaning = 2f;

        public PlayerDeathState() : base() 
        {
            stateID = PlayerStateID.Death;
        }

        public override void EvaluateState() { }

        public override void OnEnter() 
        {
            GameEvent.Trigger(GameEventType.GameOver);
            playShockAnimation = GetObstacleHit().Type != ObstacleType.Shark;
            FSM.Movement.rb.HaltMovement();
        }

        public override void RunState() 
        {
            if (playShockAnimation && shockTimeRemaning > 0)
            {
                FSM.Animator.Play("Electrocuted");
                shockTimeRemaning -= Time.deltaTime;
            }
            else if (Mathf.Approximately(FSM.Rigidbody.velocity.y, 0))
            {
                FSM.Animator.Play("Death");
            }
            else
            {
                FSM.Animator.Play("Fall");
            }
        }

        public override void FixedRunState() { }

        public override void OnExit() { }

        public override void Initialise() { }

        public override void Dispose() { }
    }
}