using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private readonly int JumpHash = Animator.StringToHash("Jump");
    private const float CrossFadeDuration = 0.2f;

    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("You are jumping!! wooo");
        stateMachine.Velocity = new Vector3(stateMachine.Velocity.x, stateMachine.JumpForce, stateMachine.Velocity.z);

        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossFadeDuration);
        stateMachine.InputReader.OnJumpPerformed += SwitchToFlyState;
    }

    public override void Tick()
    {
        ApplyGravity();
        ApplyHealthRegen();
        ApplyHeatCooling();
        if (stateMachine.Velocity.y <= 0f)
        {
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
        }
        CalculateMoveDirection();
        FaceMoveDirection();
        Move();





    }

    public override void Exit() 
    {
        stateMachine.InputReader.OnJumpPerformed -= SwitchToFlyState;
    }

    private void SwitchToFlyState()
    {
        stateMachine.SwitchState(new PlayerFlyState(stateMachine));
    }


}
