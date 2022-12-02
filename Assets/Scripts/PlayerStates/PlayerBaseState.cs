using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected readonly PlayerStateMachine stateMachine;

    protected PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void CalculateMoveDirection()
    {
        Vector3 cameraForward = stateMachine.mainCamera.forward; // OLD: new(stateMachine.mainCamera.forward.x, 0f, stateMachine.mainCamera.forward.z);
        Vector3 cameraRight = stateMachine.mainCamera.right; //OLD: new(stateMachine.mainCamera.right.x, 0f, stateMachine.mainCamera.right.z);

        Vector3 moveDirection = (cameraForward * stateMachine.InputReader.MoveComposite.y) + (cameraRight * stateMachine.InputReader.MoveComposite.x);

        stateMachine.Velocity.x = moveDirection.x * stateMachine.MovementSpeed;
        stateMachine.Velocity.z = moveDirection.z * stateMachine.MovementSpeed;
     }

    protected void FaceMoveDirection()
    {
        Vector3 faceDirection = new(stateMachine.Velocity.x, 0f, stateMachine.Velocity.z);

        if (faceDirection == Vector3.zero)
            return;

        stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, Quaternion.LookRotation(faceDirection), stateMachine.LookRotationDampFactor * Time.deltaTime);
    }

    protected void CalculateMoveDirection_fly()
    {
        Vector3 cameraForward = stateMachine.mainCamera.forward; // OLD: new(stateMachine.mainCamera.forward.x, 0f, stateMachine.mainCamera.forward.z);
        Vector3 cameraRight = stateMachine.mainCamera.right; //OLD: new(stateMachine.mainCamera.right.x, 0f, stateMachine.mainCamera.right.z);

        Vector3 moveDirection = (cameraForward * stateMachine.InputReader.MoveComposite.y) + (cameraRight * stateMachine.InputReader.MoveComposite.x);
        Vector3 flyDirection = (cameraForward * stateMachine.baseFlySpeed);

        stateMachine.Velocity = moveDirection * stateMachine.MovementSpeed + flyDirection;
        //stateMachine.Velocity.x = moveDirection.x * stateMachine.MovementSpeed;
        //stateMachine.Velocity.z = moveDirection.z * stateMachine.MovementSpeed;
        //stateMachine.Velocity.y = moveDirection.y * stateMachine.MovementSpeed;

        //stateMachine.Velocity += 
    }

    protected void FaceMoveDirection_fly()
    {
        Vector3 faceDirection = new(stateMachine.Velocity.x, stateMachine.Velocity.y, stateMachine.Velocity.z);
        faceDirection.y = Mathf.Clamp(faceDirection.y, -1.0f, 1.0f); 

        if (faceDirection == Vector3.zero)
            return;

        stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, Quaternion.LookRotation(faceDirection), stateMachine.LookRotationDampFactor * Time.deltaTime);
    }

    protected void Move()
    {
        stateMachine.Controller.Move(stateMachine.Velocity * Time.deltaTime);
    }

    protected void Move_fly()
    {
        stateMachine.Controller.Move(stateMachine.Velocity * Time.deltaTime);
        //stateMachine.Controller.Move(stateMachine.mainCamera.forward * Time.deltaTime * stateMachine.baseFlySpeed);
    }

    protected void ApplyGravity()
    {
        if (stateMachine.Velocity.y > Physics.gravity.y)
        {
            stateMachine.Velocity.y += Physics.gravity.y * Time.deltaTime;
        }
    }

    protected void ApplyHealthRegen()
    {
        if (stateMachine.HealthComponent.currentHealth < stateMachine.HealthComponent.maxHealth)
        {
            stateMachine.HealthComponent.currentHealth += stateMachine.HealthComponent.healthRegen * Time.deltaTime;
        }
    }

    protected void ApplyHeatCooling()
    {
        if (stateMachine.HeatComponent.currentHeat > 0)
        {
            stateMachine.HeatComponent.currentHeat -= stateMachine.HeatComponent.heatCoolingRate * Time.deltaTime;
        }
    }

}