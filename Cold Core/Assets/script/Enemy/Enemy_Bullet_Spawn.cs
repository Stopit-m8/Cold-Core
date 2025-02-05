using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet_Spawn : MonoBehaviour
{
    public Tank_Patrol Tank;
    [SerializeField] private Transform transform;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float FireRate;
    [SerializeField] private float FireRateTimer;

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
    // Start is called before the first frame update
    void Start()
    {
        FireRateTimer = FireRate - 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Tank.HasLOS == true)
        {
            FireRateTimer += Time.deltaTime;
            if(FireRateTimer > FireRate)
            {
                audioManager.PlaySFX(audioManager.Tankshoot);
                Instantiate(Bullet, transform.position, transform.rotation);
                FireRateTimer = 0f;
             }
        }
        else
        {
            FireRateTimer = FireRate - 0.5f;
        }
            
    }
}
