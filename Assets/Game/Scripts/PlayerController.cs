using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : Activatable
{
    [Header("References")]
    public Player player;

    [Header("Movement & Jump")]
    public float movementSpeed;
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

    protected override void OnActivate()
    {
        base.OnActivate();

        rb2D = player.rigidBody2D;
    }

    protected override void Tick()
    {
        if (!isDashing)
        {
            Move();
        }

        if (dashCountDown > 0f)
        {
            dashCountDown -= Time.deltaTime;
        }
    }

    private void Move()
    {
        rb2D.velocity = new Vector2(movementSpeed, rb2D.velocity.y);
        TriggerChunkSpawn();
    }

    private void TriggerChunkSpawn()
    {
        Debug.Log($"trigger spawn: {ChunkSpawner.Instance.TriggerSpawnPosition}");
        if (player.transform.position.x >= ChunkSpawner.Instance.TriggerSpawnPosition)
        {
            Debug.Log("Yes");
            ChunkSpawner.Instance.Spawn();
        }
    }

    public bool Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

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
        if (isDashing || dashCountDown > 0f)
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
        player.rigidBody2D.velocity = new Vector2(dashSpeed, player.rigidBody2D.velocity.y);

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

    protected override void OnDeactivate()
    {
        base.OnDeactivate();

        Stop();
        rb2D = null;
    }
} 