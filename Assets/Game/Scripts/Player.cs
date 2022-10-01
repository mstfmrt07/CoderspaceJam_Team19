using UnityEngine;

public class Player : MSingleton<Player>, IGameEventsHandler
{
    [Header("References")]
    public Hitbox hitbox;
    public PlayerController controller;
    public CharacterAnimator animator;
    public Rigidbody2D rigidBody2D;

    private bool isAlive;
    public bool IsAlive => isAlive;

    private void Awake()
    {
        hitbox.OnDestroy += Die;
        isAlive = true;

        SubscribeGameEvents();
    }
    public void SubscribeGameEvents()
    {
        GameEvents.OnGameLoad += OnGameLoad;
        GameEvents.OnGameStarted += OnGameStarted;
        GameEvents.OnGameFailed += OnGameFailed;
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
        controller.IsActive = false;
        animator.PlayAnim(AnimationState.IDLE);
    }

    public void OnGameStarted()
    {
        controller.IsActive = true;
        animator.PlayAnim(AnimationState.RUN);
    }

    public void OnGameFailed()
    {
        controller.IsActive = false;
        animator.PlayAnim(AnimationState.ANGEL);
    }
}
