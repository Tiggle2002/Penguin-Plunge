using PenguinPlunge.Core;
using System.Linq;
using UnityEngine;
using PenguinPlunge.Utility;

namespace PenguinPlunge.AI
{
    public class PlayerFSMState : FSMState<PlayerTransition, PlayerStateID>
    {
        protected PlayerFSM FSM;
        protected LayerMask obstacleLayer;
        protected static Obstacle obstacleHit;

        public PlayerFSMState()
        {
            this.FSM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFSM>();
            obstacleLayer = LayerMask.GetMask("Obstacle");
        }

        #region FSM Events
        public override void EvaluateState() { }

        public override void OnEnter() { }

        public override void RunState() => CheckForObstacle();

        public override void FixedRunState() { }

        public override void OnExit() { }
        #endregion

        #region Interface Methods
        public override void Initialise()
        {

        }

        public override void Dispose()
        {

        }
        #endregion

        protected bool HitByObstacle() => obstacleHit != null;

        private void CheckForObstacle()
        {
            Bounds overlapBox = new Bounds(FSM.Collider.bounds.center + (Vector3)FSM.Collider.offset, FSM.Collider.bounds.size);

            Collider2D[] colliders = Physics2D.OverlapBoxAll(overlapBox.center, overlapBox.size, 0, obstacleLayer);

            if (colliders.Length > 0)
            {
                foreach (var collider in colliders)
                {
                    if (collider.InLayer(obstacleLayer) && collider.GetComponentInParent<Obstacle>())
                    {
                        obstacleHit = collider.GetComponentInParent<Obstacle>();
                        break;
                    }
                }
            }
        }
    }
}
