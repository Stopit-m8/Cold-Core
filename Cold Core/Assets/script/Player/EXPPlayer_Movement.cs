using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPPlayer_Movement : MonoBehaviour
{   
    [SerializeField]
    private float groundSpeed;

    [SerializeField] private float jumpSpeed;

    [Range(0f,1f)]
    [SerializeField]
    private float drag;

    [SerializeField]
    private Rigidbody2D body;

    [SerializeField] private CapsuleCollider2D groundCheck;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask groundMask;

    float xInput;
    float yInput;

    private bool isFacingRight = true;  

    // Start is called before the first frame update
    void Start()
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody2D>(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        MoveWithInput();
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
       
        if (Mathf.Abs(xInput) > 0)
        {
            body.velocity = new Vector2(xInput * groundSpeed, body.velocity.y);

           
            if ((xInput > 0 && !isFacingRight) || (xInput < 0 && isFacingRight))
            {
                Flip(); 
            }
        }

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
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
}
