using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    private float timer;

    public GameObject explosion;
    [SerializeField] private float destroyBullet;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = MathF.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

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

    // Update is called once per frame
    void Update()
    {
        
        
        timer += Time.deltaTime;
        if (timer > destroyBullet)
        {
            audioManager.PlaySFX(audioManager.bulletImpact);
            Destroy(gameObject);
            timer = 0;
    
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
           Instantiate(explosion, transform.position, Quaternion.identity);
            audioManager.PlaySFX(audioManager.bulletImpact);
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == 6)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            audioManager.PlaySFX(audioManager.bulletImpact);
            Destroy(gameObject);
        }
    }
}
