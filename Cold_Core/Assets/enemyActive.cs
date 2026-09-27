using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyActive : MonoBehaviour
{
    public bool acc;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MainCamera"))
        {
            acc = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MainCamera"))
        {
            acc = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MainCamera"))
        {
            acc = false;
        }
    }
}
