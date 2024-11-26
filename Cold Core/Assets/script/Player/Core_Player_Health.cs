using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Core_Player_Health : MonoBehaviour
{
    public Image HealthBar;
    public float HealthAmount = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void TakeDamage(float Damage)
    {
        HealthAmount -= Damage;
        HealthBar.fillAmount = HealthAmount / 100f;
    }

    public void HealAmount(float Heal)
    {
        HealthAmount += Heal;
        HealthAmount = Mathf.Clamp(HealthAmount, 0, 100);
        HealthBar.fillAmount = HealthAmount / 100f;
    }

    public void Check(Collider2D obj)
    {
        if (obj.gameObject.tag == "Bullet")
        {
            TakeDamage(20);
        }
    }
}
