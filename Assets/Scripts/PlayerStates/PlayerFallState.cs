using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    private readonly int FallHash = Animator.StringToHash("Fall");
    private const float CrossFadeDuration = 0.5f;

    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    protected void CalculateFallDamage()
    {

    }

    public override void Enter()
    {
        Debug.Log("You are falling D:");
        stateMachine.Velocity.y = 0f;

        stateMachine.Animator.CrossFadeInFixedTime(FallHash, CrossFadeDuration);

    }

    public override void Tick()
    {
        ApplyGravity();
        CalculateMoveDirection();
        FaceMoveDirection();
        Move();

        if (stateMachine.Controller.isGrounded)
        {
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }
    }

    public override void Exit() 
    {
       // CalculateFallDamage();

     }
}
