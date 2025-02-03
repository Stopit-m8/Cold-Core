using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copter_patrol : MonoBehaviour
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
        
    }
}
