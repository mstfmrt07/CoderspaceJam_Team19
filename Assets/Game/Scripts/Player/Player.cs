using UnityEngine;

public class Player : MSingleton<Player>, IGameEventsHandler, IResettable
{
    [Header("References")]
    public Hitbox hitbox;
    public PlayerController controller;
    public CharacterAnimator animator;
    public Rigidbody2D rigidBody2D;

    private bool isAlive;
    public bool IsAlive => isAlive;

    private Vector3 initialPos;

    private void Awake()
    {
        initialPos = transform.position;
        SubscribeGameEvents();
    }

    public void SubscribeGameEvents()
    {
        GameEvents.OnGameLoad += OnGameLoad;
        GameEvents.OnGameStarted += OnGameStarted;
        GameEvents.OnGameFailed += OnGameFailed;
        GameEvents.OnGameRecovered += OnGameRecovered;
    }

    public void AttemptJump()
    {
        if (controller.Jump())
        {
            animator.PlayAnim(AnimationState.JUMP);
        }
    }

    public void AttemptDash()
    {
        if (controller.Dash())
        {
            animator.PlayAnim(AnimationState.DASH);
        }
    }

    private void Update()
    {
        if (isAlive)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
                AttemptDash();

            if (Input.GetKeyDown(KeyCode.Space))
                AttemptJump();
        }
    }

    private void Die()
    {
        isAlive = false;
    }

    public void OnGameLoad()
    {
        isAlive = true;
        controller.IsActive = false;
        transform.position = initialPos;

        animator.PlayAnim(AnimationState.IDLE);
    }

    public void OnGameStarted()
    {
        controller.IsActive = true;

        hitbox.OnDestroy += Die;
        controller.OnJumpEnded += Run;

        animator.PlayAnim(AnimationState.RUN);
    }

    public void OnGameFailed()
    {
        controller.IsActive = false;
        hitbox.OnDestroy -= Die;
        controller.OnJumpEnded -= Run;
    }

    public void OnGameRecovered()
    {
        throw new System.NotImplementedException();
    }

    public void ApplyReset()
    {
        isAlive = true;
        animator.PlayAnim(AnimationState.IDLE);
    }

    private void Run()
    {
        animator.PlayAnim(AnimationState.RUN);
    }
}
