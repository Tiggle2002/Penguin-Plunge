using PenguinPlunge.AI;
using System.Collections;
using UnityEngine;

public enum PlayerTransition { Grounded, JumpPressed, Hurt }

public enum PlayerStateID { Run, Swim, Death }

public class PlayerFSM : FSM<PlayerTransition, PlayerStateID>
{

    #region References 
    public Rigidbody2D Rigidbody { get; private set; }
    public BoxCollider2D Collider { get; private set; }
    public Animator Animator { get; private set; }
    #endregion

    #region Update Methods
    protected override void AwakeFSM()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<BoxCollider2D>();
        Animator = GetComponentInChildren<Animator>();
    }

    protected override void InitialiseFSM()
    {
        ConstructStates();
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
    #endregion

    protected void ConstructStates()
    {
        PlayerRunState runState = new();
        runState.AddTransition(PlayerTransition.Hurt, PlayerStateID.Death);
        runState.AddTransition(PlayerTransition.Grounded, PlayerStateID.Run);

        PlayerSwimState swimState = new();
        swimState.AddTransition(PlayerTransition.Hurt, PlayerStateID.Death);
        swimState.AddTransition(PlayerTransition.Grounded, PlayerStateID.Run);

        PlayerDeathState deathState = new();

        AddState(runState);
        AddState(swimState);
        AddState(deathState);
    }
}
