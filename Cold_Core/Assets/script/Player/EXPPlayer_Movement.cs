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

    [SerializeField] private AudioManager audioManager;

    private AudioSource walkAudioSource; // AudioSource for walking sound
    private float stepInterval = 0.4f; // Time between footsteps
    private float stepTimer = 0f; // Timer to track intervals

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();

        if (audioManager == null)
        {
            Debug.LogError("AudioManager is NOT found in the scene!");
        }
        else
        {
            Debug.Log("AudioManager successfully found using FindObjectOfType!");
        }

        // Initialize the AudioSource for walking sound
        walkAudioSource = gameObject.AddComponent<AudioSource>();
        walkAudioSource.loop = false; // Don't loop, we want to play footsteps in intervals
        walkAudioSource.playOnAwake = false; // Prevent it from playing on awake
    }

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
            ForceDeathAnimation();
            body.gravityScale = 5;
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

            // Footstep sound on every step (check if enough time has passed)
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval && isGrounded)
            {
                // Play footstep sound when enough time has passed
                walkAudioSource.clip = audioManager.walking;  // Assign the footstep clip
                walkAudioSource.Play();  // Play the sound

                stepTimer = 0f; // Reset the timer after playing the sound
            }

            // Flip logic
            if ((xInput > 0 && !isFacingRight) || (xInput < 0 && isFacingRight))
            {
                Flip();
            }
        }
        else
        {
            // Stop the walking sound when the player stops moving
            if (walkAudioSource.isPlaying || !isGrounded)
            {
                walkAudioSource.Stop();
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
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;

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
            walkAudioSource.Stop();
            audioManager.PlaySFX(audioManager.jumping);
            isJumping = true;
            jumpHoldTimer = 0f;
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpHoldTimer < jumpHoldTime)
            {
                walkAudioSource.Stop();
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

    void ForceDeathAnimation()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            animator.Play("Death", 0, 0);
        }
    }

    public void OnPlayerDeath()
    {
        isDead = true;

        body.velocity = new Vector2(0, body.velocity.y); // Keep the Y velocity for falling
        ForceDeathAnimation();
        body.gravityScale = 5;

        xInput = 0;
        yInput = 0;
    }
}
