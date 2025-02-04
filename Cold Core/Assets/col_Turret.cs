using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class col_Turret : MonoBehaviour
{
    private bool tag;
    public Enemy_Bullet_Spawn_Turret Turret;

    public Animator animator;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        tag = collision.gameObject.CompareTag("Player");
        if (tag)
        {
            Turret.HasLOS = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        tag = collision.gameObject.CompareTag("Player");
        if (tag)
        {
            Turret.HasLOS = false;
        }
    }
}
