using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPPlayer_Movement : MonoBehaviour
{
    [SerializeField]
    private float groundSpeed;

    [SerializeField] private float jumpSpeed;

    [Range(0f, 1f)]
    [SerializeField]
    private float drag;

    [SerializeField]
    private Rigidbody2D body;

    [SerializeField] private CapsuleCollider2D groundCheck;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask groundMask;

    // Direction flag to track if the player is facing right
    public bool isFacingRight = true;

    // Animator
    [SerializeField] private Animator animator;

    // Add these for jump mechanics
    private bool isJumping = false;
    private bool isButtonHeld = false;
    private float jumpHoldTime = 0.32f;
    private float jumpHoldTimer = 0f;

    float xInput;
    float yInput;

    // Flag to track if the player is dead
    public bool isDead = false;

    void Start()
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();
        }

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDead)  // Prevent movement when dead
        {
            GetInput();
            MoveWithInput();
            HandleJumping();

            UpdateAnimator();  // Update the animator parameters based on player state
        }
    }

    void FixedUpdate()
    {
        if (!isDead)  // Apply friction and check for ground only if alive
        {
            CheckGround();
            ApplyFriction();
        }
    }

    void GetInput()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    void MoveWithInput()
    {
        // Movement logic for horizontal movement
        if (Mathf.Abs(xInput) > 0)
        {
            body.velocity = new Vector2(xInput * groundSpeed, body.velocity.y);

            // Flip logic
            if ((xInput > 0 && !isFacingRight) || (xInput < 0 && isFacingRight))
            {
                Flip();
            }
        }
    }

    void ApplyFriction()
    {
        if (isGrounded && Mathf.Abs(xInput) == 0 && Mathf.Abs(yInput) == 0)
        {
            body.velocity *= drag;
        }
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    void HandleJumping()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpHoldTimer = 0f;
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpHoldTimer < jumpHoldTime)
            {
                jumpHoldTimer += Time.deltaTime;
                body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            }
        }

        if (Input.GetButtonUp("Jump") && body.velocity.y > 0)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);
            isJumping = false;
        }

        if (isGrounded && body.velocity.y == 0)
        {
            isJumping = false;
        }
    }

    void UpdateAnimator()
    {
        animator.SetFloat("XSpeed", Mathf.Abs(xInput));
        animator.SetBool("XJumping", !isGrounded);
        animator.SetBool("XIsFalling", body.velocity.y < 0);

        if (Mathf.Abs(xInput) == 0 && isGrounded)
        {
            animator.SetBool("XIsIdle", true);
        }
        else
        {
            animator.SetBool("XIsIdle", false);
        }
    }
}
