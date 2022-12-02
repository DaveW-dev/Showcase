using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("You are dead :( Press space to ressurect!");
        stateMachine.HealthComponent.currentHealth = 0;
        stateMachine.Velocity.x = 0;
        stateMachine.Velocity.z = 0;
    }

    public override void Tick()
    {
        ApplyHeatCooling();
        ApplyGravity();
        Move();

        //Respawn:
        if (Input.GetKey(KeyCode.Space))
        {
            //Move to respawn point
            stateMachine.transform.position = stateMachine.respawnPoint.transform.position;
            //Set health back to max
            stateMachine.HealthComponent.currentHealth = stateMachine.HealthComponent.maxHealth;
            //Switch to move state 
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }

    }

    public override void Exit() 
    {

    }
}
