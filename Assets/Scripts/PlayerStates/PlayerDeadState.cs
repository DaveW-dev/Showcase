using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    //private readonly int DeadHash = Animator.StringToHash("Dead");
    private const float CrossFadeDuration = 0.5f;

    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("You are dead :( Press space to ressurect!");
    }

    public override void Tick()
    {
        ApplyGravity();

        if (Input.GetKey(KeyCode.Space))
        {
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }

    }

    public override void Exit() 
    {
    
    }
}
