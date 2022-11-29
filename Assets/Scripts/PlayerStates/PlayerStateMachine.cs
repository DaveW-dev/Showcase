using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(HealthComponent))]
public class PlayerStateMachine : StateMachine
{
    public Vector3 Velocity;
    public Transform respawnPoint;
    public float MovementSpeed { get; private set; } = 5f;
    public float JumpForce { get; private set; } = 6f;
    public float LookRotationDampFactor { get; private set; } = 15f;
    public Transform MainCamera { get; private set; }
    public InputReader InputReader { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public HealthComponent HealthComponent { get; private set; }

    private void Start()
    {
        MainCamera = Camera.main.transform;

        InputReader = GetComponent<InputReader>();
        Animator = GetComponent<Animator>();
        Controller = GetComponent<CharacterController>();
        //maxHealth = GetComponent<HealthComponent>();
        //currentHealth = GetComponent<HealthComponent>();
        //minHealth = GetComponent<HealthComponent>();
        //healthRegen = GetComponent<HealthComponent>();

        SwitchState(new PlayerDeadState(this));
    }
}