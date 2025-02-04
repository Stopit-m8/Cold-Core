using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    [SerializeField] private int Force;
    [SerializeField] private float DTime;
    [SerializeField] private int Damage;
    private Rigidbody2D rb;
    private float Timer;
    private bool tag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * Force;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Timer += Time.deltaTime;
        if (Timer > DTime)
        {
            Destroy(gameObject);
            Timer = 0;
        }
    }
}
