using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(HeatComponent))]
public class PlayerStateMachine : StateMachine
{
    public Vector3 Velocity;
    public Transform respawnPoint;
    public float baseFlySpeed { get; private set; } = 10f;
    public float MovementSpeed { get; private set; } = 5f;
    public float JumpForce { get; private set; } = 6f;
    public float LookRotationDampFactor { get; private set; } = 15f;
    public Transform mainCamera;
    public InputReader InputReader { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public HealthComponent HealthComponent { get; private set; }
    public HeatComponent HeatComponent { get; private set; }

    private void Start()
    {
        mainCamera = mainCamera.GetComponent<Transform>();
        InputReader = GetComponent<InputReader>();
        Animator = GetComponent<Animator>();
        Controller = GetComponent<CharacterController>();
        HealthComponent = GetComponent<HealthComponent>();
        HeatComponent = GetComponent<HeatComponent>();

        //Default state (dead):
        SwitchState(new PlayerDeadState(this));
    }


    //Death check needs to run in all states
    private void LateUpdate()
    {
        if (HealthComponent.currentHealth < HealthComponent.minHealth)
        {
            SwitchState(new PlayerDeadState(this));
        }

        //Debug.Log("mainCamera.forward > " + mainCamera.forward);
        //Debug.Log("mainCamera.forward.z > " + mainCamera.forward.z);
        //Debug.Log("mainCamera.right > " + mainCamera.right);
        //Debug.Log("mainCamera.right.z > " + mainCamera.right.z);
        //Debug.Log("mainCamera.forward.y > " + mainCamera.forward.y);
        //Debug.Log("mainCamera.up.y > " + mainCamera.up.y);


    }
}