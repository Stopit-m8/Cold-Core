using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int eMaxHealth = 150;
    public int eCurrentHealth;
    [SerializeField] private GameObject item;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        eCurrentHealth = eMaxHealth;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(9);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (eCurrentHealth <= 0)
        {
            eCurrentHealth = 0;
            animator.SetBool("tankdead", true);
            Instantiate(item,transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void TakeDamage(int damage)
    {
        eCurrentHealth -= damage;
    }
}
