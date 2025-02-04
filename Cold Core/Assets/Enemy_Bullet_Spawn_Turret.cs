using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet_Spawn_Turret : MonoBehaviour
{
    public bool HasLOS = false;
    [SerializeField] private Transform transform;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float FireRate;
    private float FireRateTimer;
    public enemyActive acc;
    // Start is called before the first frame update
    void Start()
    {
        FireRateTimer = FireRate - 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
            FireRateTimer += Time.deltaTime;
            if (FireRateTimer > FireRate && acc.acc)
            {
                Instantiate(Bullet, transform.position, transform.rotation);
                FireRateTimer = 0f;
            }

    }
}
