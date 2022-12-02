using UnityEngine;

public class PlayerFlyState : PlayerBaseState
{

    //NEED FLY ANIMS!!!!
    private const float AnimationDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    public PlayerFlyState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("You are flying!!");
        stateMachine.Velocity.y = 0f;

        //stateMachine.Animator.CrossFadeInFixedTime(MoveBlendTreeHash, CrossFadeDuration);
        stateMachine.InputReader.OnJumpPerformed += SwitchToFallState;
    }

    public override void Tick()
    {
        //Apply heat increase while flying
        stateMachine.HeatComponent.currentHeat += stateMachine.HeatComponent.heatRate * Time.deltaTime;
        //Switch to fall state if current heat > maxHeat
        if (stateMachine.HeatComponent.currentHeat >= stateMachine.HeatComponent.maxHeat)
        {
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
        }

        ApplyHealthRegen();
        CalculateMoveDirection_fly();
        FaceMoveDirection_fly();
        Move_fly();

        //stateMachine.Animator.SetFloat(MoveSpeedHash, stateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? 1f : 0f, AnimationDampTime, Time.deltaTime);
    }

    public override void Exit()
    {

        stateMachine.InputReader.OnJumpPerformed -= SwitchToFallState;
    }

    private void SwitchToFallState()
    {
        stateMachine.SwitchState(new PlayerFallState(stateMachine));
    }


}
