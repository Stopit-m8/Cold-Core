using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Patrol : MonoBehaviour
{
    public GameObject PointA;
    public GameObject PointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public bool HasLOS;
    public Animator animator;
    [SerializeField] private Collider2D LOS;  // Reference to LOS Collider
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        HasLOS = false;
        rb = GetComponent<Rigidbody2D>();
        currentPoint = PointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (HasLOS == true)
        {
            rb.velocity = new Vector2(0f, 0f);
        }
        else
        {
            Vector2 point = currentPoint.position - transform.position;

            // Move towards PointB
            if (currentPoint == PointB.transform)
            {
                rb.velocity = new Vector2(speed, 0f);
            }
            // Move towards PointA
            else
            {
                rb.velocity = new Vector2(-speed, 0f);
            }

            // Check if the Tank has reached PointB or PointA and flip accordingly
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointB.transform)
            {
                flip();
                currentPoint = PointA.transform;
            }
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointA.transform)
            {
                flip();
                currentPoint = PointB.transform;
            }

            // Set animation speed
            if (speed > 0.01)
            {
                animator.SetBool("Speed", true);
            }
            else if (speed < 0)
            {
                animator.SetBool("Speed", false);
            }
        }
    }

    // Visualize patrol points in editor
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);
        Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
    }

    // Flip the Tank and the LOS's collider along with it
    private void flip()
    {
        // Flip the Tank's rotation
        transform.Rotate(0f, 180f, 0f);

        // Flip the LOS collider's local scale to mirror the Tank's flip
        if (LOS != null)
        {
            // Flip the LOS collider horizontally
            Vector3 losScale = LOS.transform.localScale;
            losScale.x = -losScale.x;
            LOS.transform.localScale = losScale;
        }
    }
}
