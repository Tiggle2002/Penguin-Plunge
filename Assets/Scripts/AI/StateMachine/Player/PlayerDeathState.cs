using System.Collections;
using UnityEngine;

namespace PenguinPlunge.AI
{
    public class PlayerDeathState : FSMState<PlayerTransition, PlayerStateID>
    {
        public PlayerDeathState() : base() 
        {
            stateID = PlayerStateID.Death;
        }

        public override void EvaluateState() { }

        public override void OnEnter() { }

        public override void RunState() { }

        public override void FixedRunState() { }

        public override void OnExit() { }

        public override void Initialise() { }

        public override void Dispose() { }
    }
}