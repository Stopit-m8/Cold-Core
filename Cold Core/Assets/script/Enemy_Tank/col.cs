using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class col : MonoBehaviour
{
    private bool tag;
    public Tank_Patrol Tank;

  
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        tag = collision.gameObject.CompareTag("Player");
        if (tag)
        {
            Tank.HasLOS = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        tag = collision.gameObject.CompareTag("Player");
        if (tag)
        {
            Tank.HasLOS = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        tag = collision.gameObject.CompareTag("Player");
        if (tag)
        {
            Tank.HasLOS = false;
        }
    }
}
