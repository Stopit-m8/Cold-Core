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
        else
        {
            // Force death animation immediately when the player is dead (even mid-air)
            ForceDeathAnimation();

            // Increase gravity scale so the player falls fast
            body.gravityScale = 5;

            // Stop horizontal movement (but don't stop the animator)
            body.velocity = new Vector2(0, body.velocity.y); // Keep the Y velocity for falling
        }
    }

    void FixedUpdate()
    {
        if (!isDead)  // Apply friction and check for ground only if alive
        {
            CheckGround();
            ApplyFriction();
        }
        else
        {
            // Ensure ground detection works even if the player is not moving horizontally
            CheckGround();
        }
    }

    void GetInput()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    void MoveWithInput()
    {
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
        // Ground detection is critical, let's force a debug check here
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;

        // Debugging: Check if ground state changes are properly detected
        if (wasGrounded != isGrounded)
        {
            Debug.Log(isGrounded ? "Player is grounded" : "Player is in the air");
        }
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

        // Debugging: Log falling status for mid-air state
        Debug.Log("isFalling: " + (body.velocity.y < 0 && !isGrounded));

        // Check if player is falling even if not moving horizontally
        // If player is falling and not grounded, set the falling animation to true
        animator.SetBool("XIsFalling", body.velocity.y < 0 && !isGrounded);

        if (Mathf.Abs(xInput) == 0 && isGrounded)
        {
            animator.SetBool("XIsIdle", true);
        }
        else
        {
            animator.SetBool("XIsIdle", false);
        }
    }

    // Force the death animation to play immediately, even mid-air
    void ForceDeathAnimation()
    {
        // Forcefully stop any animation and play the death animation immediately
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            // This will ensure that the "Death" animation plays from the beginning
            animator.Play("Death", 0, 0);  // Play "Death" animation from the start
        }
    }

    // Call this method when the player dies
    public void OnPlayerDeath()
    {
        isDead = true;

        // Immediately stop all movement once the player is dead
        body.velocity = new Vector2(0, body.velocity.y); // Keep the Y velocity for falling

        // Force the death animation to play immediately
        ForceDeathAnimation();

        // Make sure gravity affects the player immediately (fall faster)
        body.gravityScale = 5;

        // Disable player input (optional: helps in ensuring no movement during death)
        xInput = 0;
        yInput = 0;
    }
}
