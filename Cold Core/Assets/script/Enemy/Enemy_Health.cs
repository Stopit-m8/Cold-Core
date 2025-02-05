using System.Collections;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int eMaxHealth = 150;
    public int eCurrentHealth;
    [SerializeField] private GameObject item;

    public Animator animator;
    private Rigidbody2D rb;

    private Tank_Patrol tankPatrolScript;

    // Reference to the death animation clip
    public AnimationClip deathAnimationClip;

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

    void Start()
    {
        eCurrentHealth = eMaxHealth;
        rb = GetComponent<Rigidbody2D>();
        tankPatrolScript = GetComponent<Tank_Patrol>(); // Reference to Tank_Patrol script
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(9);
        }
    }

    void Update()
    {
        if (eCurrentHealth <= 0)
        {
            if (!animator.GetBool("tankdead"))
            {
                audioManager.PlaySFX(audioManager.tankExplode);
                animator.SetBool("tankdead", true);  // Trigger death animation
                eCurrentHealth = 0;
                Instantiate(item, transform.position, Quaternion.identity);

                // Disable Tank_Patrol script and freeze Rigidbody
                if (tankPatrolScript != null)
                {
                    tankPatrolScript.enabled = false;
                }
                rb.constraints = RigidbodyConstraints2D.FreezeAll; // Prevent movement

                StartCoroutine(DestroyAfterAnimation());
            }
        }
    }

    private void TakeDamage(int damage)
    {
        eCurrentHealth -= damage;
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // If you have the death animation clip reference set in the Inspector, get its length
        if (deathAnimationClip != null)
        {
            float animationDuration = deathAnimationClip.length;
            yield return new WaitForSeconds(animationDuration);  // Wait for the full duration of the death animation
        }
        else
        {
            // Fallback: Use Animator state length if clip reference is not assigned
            float animationDuration = animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationDuration);  // Wait for the animation to finish
        }

        Destroy(gameObject);  // Destroy the object after animation finishes
    }
}