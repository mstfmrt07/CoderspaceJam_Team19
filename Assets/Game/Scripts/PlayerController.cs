using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PlayerController : Activatable, IResettable
{
    [Header("References")]
    public Player player;

    [Header("Movement")]
    public float movementSpeed;

    [Header("Jump Skill")]
    public float jumpForce;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    [Header("Dash Skill")]
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;
    public float minDistanceBetweenImages;

    private float dashCountDown;
    private bool isDashing;
    private float lastImageXpos;

    private bool isGrounded;

    private Rigidbody2D rb2D;

    public Action OnReachedChunkSpawnPoint;
    public Action OnJumpEnded;

    protected override void OnActivate()
    {
        base.OnActivate();

        rb2D = player.rigidBody2D;
    }

    protected override void Tick()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded && rb2D.velocity.y < 0f)
        {
            Debug.Log("Player landed");
            OnJumpEnded?.Invoke();
        }

        if (!isDashing)
            Move();

        if (dashCountDown > 0f)
            dashCountDown -= Time.deltaTime;
    }

    private void Move()
    {
        rb2D.velocity = new Vector2(movementSpeed, rb2D.velocity.y);
        TriggerChunkSpawn();
    }

    public bool Jump()
    {
        if (!IsActive)
            return false;

        if (isGrounded)
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);

        return isGrounded;
    }

    private void Stop()
    {
        if (rb2D == null)
            return;

        rb2D.velocity = Vector2.zero;
    }

    public bool Dash()
    {
        if (!IsActive || isDashing || dashCountDown > 0f)
            return false;

        StartCoroutine(DashCoroutine());
        return true;
    }

    IEnumerator DashCoroutine()
    {
        isDashing = true;
        var dashTimer = dashDuration;
        dashCountDown = dashCooldown;

        float gravity = player.rigidBody2D.gravityScale;
        player.rigidBody2D.gravityScale = 0f;
        player.rigidBody2D.velocity = new Vector2(dashSpeed, 0f);

        while (dashTimer > 0f)
        {
            dashTimer -= Time.deltaTime;

            if (Mathf.Abs(player.transform.position.x - lastImageXpos) >= minDistanceBetweenImages)
            {
                ObjectPool.Instance.GetFromPool();
                lastImageXpos = transform.position.x;
            }

            yield return new WaitForEndOfFrame();
        }

        player.rigidBody2D.gravityScale = gravity;
        isDashing = false;
    }

    private void TriggerChunkSpawn()
    {
        if (player.transform.position.x >= ChunkSpawner.Instance.TriggerSpawnPosition)
            OnReachedChunkSpawnPoint?.Invoke();
    }


    protected override void OnDeactivate()
    {
        base.OnDeactivate();

        Stop();
    }

    public void ApplyReset()
    {
        rb2D = null;

        dashCountDown = 0f;
        lastImageXpos = 0f;

        isDashing = false;
        isGrounded = false;
    }
} 