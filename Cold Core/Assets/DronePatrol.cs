using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePatrol : MonoBehaviour
{
    public GameObject PointA;
    public GameObject PointB;
    public GameObject PointC;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = PointA.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 direction = currentPoint.transform.position - transform.position;
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == PointA.transform)
        {
            
            rb.velocity = new Vector2(point.x, point.y).normalized * speed;
        }
        else if (currentPoint == PointB.transform)
        {
            rb.velocity = new Vector2(point.x, point.y).normalized * speed;
        }
        else
        {
            rb.velocity = new Vector2(point.x, point.y).normalized * speed;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointA.transform)
        {
            currentPoint = PointC.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointB.transform)
        {
            currentPoint = PointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointC.transform)
        {
            currentPoint = PointB.transform;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);
        Gizmos.DrawWireSphere(PointC.transform.position, 0.5f);
        Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
        Gizmos.DrawLine(PointB.transform.position, PointC.transform.position);
        Gizmos.DrawLine(PointC.transform.position, PointA.transform.position);
    }
}
