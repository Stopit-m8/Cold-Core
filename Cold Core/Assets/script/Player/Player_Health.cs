using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Player_HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Heal(20);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            TakeDamage(20);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
        
    //}

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
    }

    void Heal(int heal)
    {
        currentHealth += heal;
        healthBar.setHealth(currentHealth);
    }
}
