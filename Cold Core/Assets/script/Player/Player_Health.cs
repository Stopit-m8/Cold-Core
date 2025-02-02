using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Player_HealthBar healthBar;
    public Animator animator;
    public DeathMenuController deathMenuController;  // Reference to the Death Menu Controller

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

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
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // If health reaches 0, trigger death and show the death menu
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animator.SetBool("PlayerIsDead", true);

            // Show the death menu
            if (deathMenuController != null)
            {
                deathMenuController.ShowDeathMenu();  // Show the death menu
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy_Bullet"))
        {
            TakeDamage(20);
        }
        if (collision.gameObject.CompareTag("Insta_Kill"))
        {
            TakeDamage(currentHealth);
        }
    }

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
