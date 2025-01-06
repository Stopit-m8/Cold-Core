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
    [SerializeField] private Collider2D LOS;
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
            Debug.Log("has LOS");
        }
        else
        {
            Debug.Log("no LOS");

            Vector2 point = currentPoint.position - transform.position;
            if (currentPoint == PointB.transform)
            {
                rb.velocity = new Vector2(speed, 0f);

            }
            else
            {
                rb.velocity = new Vector2(-speed, 0f);

            }

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



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);
        Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
    }

    private void flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }

   
}
