using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int eMaxHealth = 100;
    public int eCurrentHealth;
    // Start is called before the first frame update
    void Start()
    {
        eCurrentHealth = eMaxHealth;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(20);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (eCurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void TakeDamage(int damage)
    {
        eCurrentHealth -= damage;
    }
}
