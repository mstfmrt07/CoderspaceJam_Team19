using UnityEngine;

public class Player : MSingleton<Player>, IGameEventsHandler, IResettable
{
    [Header("References")]
    public Hitbox hitbox;
    public PlayerController controller;
    public DistanceMeter distanceMeter;
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
        Debug.Log("Attempted jump");
        if (!isAlive)
            return;

        if (controller.Jump())
        {
            animator.PlayAnim(AnimationState.JUMP);
            SoundManager.Instance.PlaySound(SoundManager.Instance.jumpClip);
        }
    }

    public void AttemptDash()
    {
        Debug.Log("Attempted dash");
        if (!isAlive)
            return;

        if (controller.Dash())
        {
            animator.PlayAnim(AnimationState.DASH);
            SoundManager.Instance.PlaySound(SoundManager.Instance.dashClip);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            AttemptDash();

        if (Input.GetKeyDown(KeyCode.Space))
            AttemptJump();
    }

    private void Die()
    {
        if (!isAlive)
            return;

        isAlive = false;
        SoundManager.Instance.PlaySound(SoundManager.Instance.dieClip);
    }

    public void OnGameLoad()
    {
        isAlive = true;
        controller.IsActive = false;
        transform.position = initialPos;
        rigidBody2D.bodyType = RigidbodyType2D.Dynamic;

        animator.PlayAnim(AnimationState.IDLE);
    }

    public void OnGameStarted()
    {
        controller.IsActive = true;

        distanceMeter.Init();
        distanceMeter.IsActive = true;

        hitbox.OnDestroy += Die;
        controller.OnJumpEnded += Run;
        controller.OnDashEnded += Run;

        animator.PlayAnim(AnimationState.RUN);
    }

    public void OnGameFailed()
    {
        controller.IsActive = false;
        distanceMeter.IsActive = true;
        rigidBody2D.bodyType = RigidbodyType2D.Kinematic;

        hitbox.OnDestroy -= Die;
        controller.OnJumpEnded -= Run;
        controller.OnDashEnded -= Run;
    }

    public void OnGameRecovered()
    {
        isAlive = true;
        controller.IsActive = true;
        distanceMeter.IsActive = true;

        rigidBody2D.bodyType = RigidbodyType2D.Dynamic;

        hitbox.ApplyReset();
        animator.ApplyReset();

        hitbox.OnDestroy += Die;
        controller.OnJumpEnded += Run;
        controller.OnDashEnded += Run;

        animator.PlayAnim(AnimationState.RUN);
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
