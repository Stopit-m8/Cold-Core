using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Player_HealthBar healthBar;
    public Animator animator;
    public DeathMenuController deathMenuController;

    private EXPPlayer_Movement playerMovement;  // Reference to the Player Movement script

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        playerMovement = GetComponent<EXPPlayer_Movement>();  // Get the Player Movement component
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

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animator.SetBool("PlayerIsDead", true);

            // Set the player's movement to be disabled upon death
            if (playerMovement != null)
            {
                playerMovement.isDead = true;  // Set the isDead flag to true
            }

            // Show the death menu
            if (deathMenuController != null)
            {
                deathMenuController.ShowDeathMenu();
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
        if (collision.gameObject.CompareTag("ItemHealth"))
        {
            Heal(20);
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
