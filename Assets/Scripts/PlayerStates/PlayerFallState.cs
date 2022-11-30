using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    private readonly int FallHash = Animator.StringToHash("Fall");
    private const float CrossFadeDuration = 0.5f;
    public float fallTime;

    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    protected void ApplyFallDamage()
    {
        //How to calculate fall damage - time in fall state? track distance between start/end?
        float fallDamage;
        float fallDamageMultiplier = 100f;

        if (fallTime > 1)
        {
            fallDamage = fallTime * fallDamageMultiplier;
            stateMachine.HealthComponent.currentHealth -= fallDamage;
        }
        else
        {
            fallDamage = 0;
        }
    }

    public override void Enter()
    {
        Debug.Log("You are falling D:");
        stateMachine.Velocity.y = 0f;

        stateMachine.Animator.CrossFadeInFixedTime(FallHash, CrossFadeDuration);

        fallTime = 0f;

    }

    public override void Tick()
    {
        ApplyHealthRegen();
        ApplyGravity();
        CalculateMoveDirection();
        FaceMoveDirection();
        Move();

        fallTime += 1 * Time.deltaTime;

        if (stateMachine.Controller.isGrounded)
        {
            //Apply fall damage here? Should be before moving to move state in case it leads to deathstate.
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }
    }

    public override void Exit() 
    {
       ApplyFallDamage();
    }
}
