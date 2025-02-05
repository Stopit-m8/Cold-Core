using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Shooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public int timerMax;
    private float timer;
    private GameObject player;
    [SerializeField] private float LOS;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if(distance < LOS)
        {
            timer += Time.deltaTime;
            if (timer > timerMax)
            {
                shoot();
                timer = 0;
            }
        }
        
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
