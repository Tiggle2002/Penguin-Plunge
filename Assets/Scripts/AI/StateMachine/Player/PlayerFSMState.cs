using UnityEngine;

public class PlayerFSMState : FSMState<PlayerTransition, PlayerStateID>
{
    protected PlayerFSM FSM;

    public PlayerFSMState()
    {
        this.FSM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFSM>();
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
}
