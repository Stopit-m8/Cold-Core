 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Core_Player_Movement : MonoBehaviour
{
    private float Core_Player_Movement_Horizontal;
    private float Core_Player_Speed = 8f;
    private float Core_Player_JumpingPower = 16f;
    private bool Core_Player_IsFacingRight = true;

    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform Ground_Check;
    [SerializeField] private LayerMask Ground_Layer; 
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Core_Player_Movement_Horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Core_Player_JumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rigidBody.velocity.y > 0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * 0.5f);
        }

        flip();
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(Core_Player_Movement_Horizontal * Core_Player_Speed, rigidBody.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(Ground_Check.position, 0.2f, Ground_Layer);
    }

    private void flip()
    {
        if (Core_Player_IsFacingRight && Core_Player_Movement_Horizontal < 0f || !Core_Player_IsFacingRight && Core_Player_Movement_Horizontal > 0f)
        {
            Core_Player_IsFacingRight = !Core_Player_IsFacingRight;
            transform.Rotate(0f, 180f, 0f);
            
        }
    }
}
