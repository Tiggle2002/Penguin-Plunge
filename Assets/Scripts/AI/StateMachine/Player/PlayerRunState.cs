using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PenguinPlunge.AI
{
    public class PlayerRunState : FSMState<PlayerTransition, PlayerStateID>
    {
        public PlayerRunState() : base()
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
