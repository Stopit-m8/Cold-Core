using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTank : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool switchC;
    private float current;
    private float movement;
    private bool facingRight = true;
    private void Awake()
    {
        movement = Input.GetAxisRaw("Horizontal");
        rb = GetComponent<Rigidbody2D>();
        switchC = true;
        
    }
    private void FixedUpdate()
    {
        if (switchC)
        {
            StartCoroutine(moveRight());
            
        }
        else if (!switchC)
        {
            StartCoroutine(moveLeft());
            
        }
        flip();
    }

    private IEnumerator moveRight()
    {
        rb.velocity = new Vector2(2f, 0f);
        yield return new WaitForSeconds(3);
        switchC = false;
    }

    private IEnumerator moveLeft()
    {
        rb.velocity = new Vector2(-2f, 0f);
        yield return new WaitForSeconds(3);
        switchC = true;
    }

    private void flip()
    {
        if (facingRight && movement < 0f || !facingRight && movement > 0f)
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);

        }
    }
}
