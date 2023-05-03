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

        public PlayerFSMState()
        {
            this.FSM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFSM>();
            obstacleLayer = LayerMask.GetMask("Obstacle");
        }

        #region FSM Events
        public override void EvaluateState() { }

        public override void OnEnter() { }

        public override void RunState() { }

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

        protected bool HitByObstacle() => FSM.Collider.IsTouchingLayers(obstacleLayer);

        public Obstacle GetObstacleHit()
        {
            Bounds overlapBox = new Bounds(FSM.Collider.bounds.center + (Vector3)FSM.Collider.offset, FSM.Collider.bounds.size);

            Collider2D[] colliders = Physics2D.OverlapBoxAll(overlapBox.center, overlapBox.size, 0);

            if (colliders.Length > 0)
            {
                return colliders.First(collider => CollisionExtensions.LayerInLayerMask(collider.gameObject.layer, obstacleLayer) && collider.GetComponent<Obstacle>()).GetComponent<Obstacle>();
            }
            return null;
        }


    }
}
