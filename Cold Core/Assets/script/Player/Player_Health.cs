using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Player_HealthBar healthBar;
    public Animator animator;
    public DeathMenuController deathMenuController;
    [SerializeField] private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();

        if (audioManager == null)
        {
            Debug.LogError("AudioManager is NOT found in the scene!");
        }
        else
        {
            Debug.Log("AudioManager successfully found using FindObjectOfType!");
        }

    }

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
            currentHealth -= 20;
            DisplayDamage(currentHealth);
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            audioManager.PlaySFX(audioManager.getHit);
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
<<<<<<< Updated upstream
            DisplayDamage(currentHealth);
        }
        if (collision.gameObject.CompareTag("Insta_Kill"))
        {
            DisplayDamage(currentHealth);
=======
            audioManager.PlaySFX(audioManager.getHit);
            TakeDamage(10);
        }
        if (collision.gameObject.CompareTag("Insta_Kill"))
        {
            audioManager.PlaySFX(audioManager.getHit);
            TakeDamage(currentHealth);
>>>>>>> Stashed changes
        }
        if (collision.gameObject.CompareTag("ItemHealth"))
        {
            audioManager.PlaySFX(audioManager.heal);
            Heal(20);
        }
    }

    void DisplayDamage(int damage)
    {
        healthBar.setHealth(currentHealth);
    }

    void Heal(int heal)
    {
        currentHealth += heal;
        healthBar.setHealth(currentHealth);
    }
}
