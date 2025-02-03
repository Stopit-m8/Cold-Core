using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet_Spawn : MonoBehaviour
{
    public Tank_Patrol Tank;
    [SerializeField] private Transform transform;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float FireRate;
    private float FireRateTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Tank.HasLOS == true)
        {
            FireRateTimer += Time.deltaTime;
            if(FireRateTimer > FireRate)
            {
                Instantiate(Bullet, transform.position, transform.rotation);
                FireRateTimer = 0f;
             }
        }
            
    }
}
