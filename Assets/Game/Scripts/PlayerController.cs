using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Transform mainBody;
    public Player player;

    [Header("Movement & Jump")]
    public float movementSpeed;
    public float jumpForce;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public int extraJumpCount;

    private float xInput;
    private bool facingRight = true;
    private bool isGrounded;
    private int extraJumpsLeft;

    private Rigidbody2D rb2D;
    private CharacterAnimator animator;

    [Header("Dash Skill")]
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;
    private float remainingDashTime;
    private bool isDashing;
    private float lastImageXpos;

    private void Awake()
    {
        animator = player.animator;
        rb2D = player.rigidBody2D;
        extraJumpsLeft = extraJumpCount;
    }

    private void Update()
    {
        if (!player.IsDead)
        {
            //Input and Animations
            xInput = Input.GetAxisRaw("Horizontal");
            if (xInput != 0 || isDashing)
            {
                animator.PlayAnim(AnimationState.RUN);
            }
            else
            {
                animator.PlayAnim(AnimationState.IDLE);
            }

            //animator.SetBool("Grounded", isGrounded);
            //animator.SetFloat("AirSpeed", rb2D.velocity.y);

            if (isGrounded)
            {
                extraJumpsLeft = extraJumpCount;
            }

            if (remainingDashTime > 0)
            {
                remainingDashTime -= Time.deltaTime;
            }

            if ((facingRight && xInput < 0) || (!facingRight && xInput > 0))
            {
                Flip();
            }

            if (Input.GetButtonDown("Jump") && !isDashing)
            {
                if (extraJumpsLeft > 0)
                {
                    Jump();
                    extraJumpsLeft--;
                }
                else if (isGrounded)
                {
                    Jump();
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && remainingDashTime <= 0)
            {
                TrailEffectPool.Instance.GetFromPool();
                lastImageXpos = transform.position.x;
                StartCoroutine(Dash());
            }

            if (isDashing)
            {
                if (Mathf.Abs(transform.position.x - lastImageXpos) > 0.1f)
                {
                    TrailEffectPool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (!isDashing && !player.IsDead)
        {
            Move();
        }
        else
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0f);
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

    }

    void Flip()
    {
        facingRight = !facingRight;

        mainBody.Rotate(new Vector3(0, 180f, 0));
    }

    void Jump()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);

        animator.PlayAnim(AnimationState.JUMP);
    }

    IEnumerator Dash()
    {
        animator.PlayAnim(AnimationState.DASH);

        remainingDashTime = dashCooldown;
        isDashing = true;

        float gravity = rb2D.gravityScale;
        float direction = facingRight ? 1f : -1f;

        rb2D.gravityScale = 0f; // reset gravity

        rb2D.velocity = new Vector2(dashSpeed * direction, rb2D.velocity.y);

        yield return new WaitForSeconds(dashDuration);
        rb2D.gravityScale = gravity;
        isDashing = false;
    }

    void Move()
    {
        rb2D.velocity = new Vector2(xInput * movementSpeed, rb2D.velocity.y);
    }
} 