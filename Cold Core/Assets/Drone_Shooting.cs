using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Shooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public int timerMax;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timerMax)
        {
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
            timer = 0;
        }
    }
}
