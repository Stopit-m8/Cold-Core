using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_Player_Movement : MonoBehaviour
{
    private float Core_Player_Movement_Horizontal;
    private float Core_Player_Speed = 8f;
    private float Core_Player_JumpingPower = 16f;
    private bool Core_Player_IsFacingRight = true;

    private bool jump = false;

    public Animator animator;
    public Player_Health health;

    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform Ground_Check;
    [SerializeField] private LayerMask Ground_Layer;

    // Start is called before the first frame update
    void Start()
    {
        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody2D>(); // Make sure Rigidbody2D is attached
        }
    }

    void Update()
    {
        //if (health.currentHealth <= 0)
        //{
        //    rigidBody.velocity = Vector2(0f,0f);
        //}
        animator.SetFloat("Speed", Mathf.Abs(Core_Player_Movement_Horizontal));

        Core_Player_Movement_Horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Core_Player_JumpingPower);
            animator.SetBool("Jump", true);
        }

        if (Input.GetButtonUp("Jump") && rigidBody.velocity.y > 0f)
        {
            // Reduces jump height if the jump button is released early
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * 0.5f);
            animator.SetBool("Jump", false);
        }

        flip();
    }

    private void FixedUpdate()
    {
        // Ensure only the horizontal movement is applied here
        rigidBody.velocity = new Vector2(Core_Player_Movement_Horizontal * Core_Player_Speed, rigidBody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        jump = true;
    }

    private bool IsGrounded()
    {
        // Check if grounded using OverlapCircle, this method checks if the player is touching the ground layer
        return Physics2D.OverlapCircle(Ground_Check.position, 0.2f, Ground_Layer);
    }

    private void flip()
    {
        // Flipping logic to face the character in the opposite direction
        if (Core_Player_IsFacingRight && Core_Player_Movement_Horizontal < 0f || !Core_Player_IsFacingRight && Core_Player_Movement_Horizontal > 0f)
        {
            Core_Player_IsFacingRight = !Core_Player_IsFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
