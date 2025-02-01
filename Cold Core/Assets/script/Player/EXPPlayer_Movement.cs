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
    [SerializeField]    private Animator animator;

    // Add these for jump mechanics
    private bool isJumping = false;  // Track if the player is jumping
    private bool isButtonHeld = false;  // Track if the jump button is being held
    private float jumpHoldTime = 0.32f;  // How long the player can hold to boost jump
    private float jumpHoldTimer = 0f;  // Timer for how long the player has been holding the jump button

    float xInput;
    float yInput;

    // Start is called before the first frame update
    void Start()
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();
        }

        animator = GetComponent<Animator>();  // Get the Animator component
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        MoveWithInput();
        HandleJumping();  // Handle the jump mechanics here

        UpdateAnimator();  // Update the animator parameters based on player state
    }

    void FixedUpdate()
    {
        CheckGround();
        ApplyFriction();
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
        // Apply friction when standing still
        if (isGrounded && Mathf.Abs(xInput) == 0 && Mathf.Abs(yInput) == 0)
        {
            body.velocity *= drag;
        }
    }

    void CheckGround()
    {
        // Check if the player is on the ground
        isGrounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    void Flip()
    {
        // Flip the character facing direction
        isFacingRight = !isFacingRight;

        // Flip the character sprite
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    // Handle Jump Mechanics (Variable Jump Height)
    void HandleJumping()
    {
        // When the player presses jump and is grounded, start the jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpHoldTimer = 0f;  // Reset jump hold timer
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);  // Jump upward
        }

        // If the jump button is held and player is still going up, increase jump height
        if (Input.GetButton("Jump") && isJumping)
        {
            // Apply boost only if the jump button is held within the max time limit
            if (jumpHoldTimer < jumpHoldTime)
            {
                jumpHoldTimer += Time.deltaTime;  // Increase timer while holding the button
                body.velocity = new Vector2(body.velocity.x, jumpSpeed);  // Keep the player going up while jump button is held
            }
        }

        // If the jump button is released early, reduce the height of the jump
        if (Input.GetButtonUp("Jump") && body.velocity.y > 0)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);  // Shorten the jump height
            isJumping = false;  // Stop further increase in jump height
        }

        // Reset the jumping state if the player is grounded
        if (isGrounded && body.velocity.y == 0)
        {
            isJumping = false;
        }
    }

    // Update Animator Parameters
    void UpdateAnimator()
    {
        // Set "Speed" parameter based on player's movement
        animator.SetFloat("XSpeed", Mathf.Abs(xInput));

        // Set "JumpUp" and "JumpDown" based on vertical velocity (jumping or falling)
        animator.SetBool("XJumping", !isGrounded);  // Is player in air or grounded?
        animator.SetBool("XIsFalling", body.velocity.y < 0);  // Set falling animation if going down

        // Set "Idle" state if not moving horizontally
        if (Mathf.Abs(xInput) == 0 && isGrounded)
        {
            animator.SetBool("XIsIdle", true);  // Player is idle
        }
        else
        {
            animator.SetBool("XIsIdle", false);  // Player is moving
        }
    }
}
