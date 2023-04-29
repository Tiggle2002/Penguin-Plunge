using PenguinPlunge.AI;
using System.Collections;
using UnityEngine;

namespace PenguinPlunge.AI
{
    public enum PlayerTransition { Grounded, JumpPressed, Hit }

    public enum PlayerStateID { Run, Swim, Death }

    public class PlayerFSM : FSM<PlayerTransition, PlayerStateID>
    {
        public Rigidbody2D Rigidbody { get; private set; }
        public BoxCollider2D Collider { get; private set; }
        public Animator Animator { get; private set; }
        public Movement Movement { get; private set; }

        protected override void AwakeFSM()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Collider = GetComponent<BoxCollider2D>();
            Animator = GetComponentInChildren<Animator>();
            Movement = GetComponentInChildren<Movement>();
            ConstructStates();
        }

        protected override void InitialiseFSM() 
        {
            SetState(PlayerStateID.Swim);
        }

        protected override void UpdateFSM()
        {
            CurrentState.RunState();
            CurrentState.EvaluateState();
        }

        protected override void FixedUpdateFSM()
        {
            CurrentState.FixedRunState();
        }

        protected void ConstructStates()
        {
            PlayerRunState runState = new();
            runState.AddTransition(PlayerTransition.Hit, PlayerStateID.Death);
            runState.AddTransition(PlayerTransition.JumpPressed, PlayerStateID.Swim);

            PlayerSwimState swimState = new();
            swimState.AddTransition(PlayerTransition.Hit, PlayerStateID.Death);
            swimState.AddTransition(PlayerTransition.Grounded, PlayerStateID.Run);

            PlayerDeathState deathState = new();

            AddState(runState);
            AddState(swimState);
            AddState(deathState);
        }
    }
}
