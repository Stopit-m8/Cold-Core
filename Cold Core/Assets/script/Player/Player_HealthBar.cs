using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    [SerializeField] private List<Sprite> healthBarSprite;

    void Start()
    {
        fill.sprite = healthBarSprite[0];
    }
    void Update()
    {
        if (slider.value <= 40)
        {
            fill.sprite = healthBarSprite[1];
        }
        else if(slider.value > 40)
        {
            fill.sprite = healthBarSprite[0];
        }
    }
    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void setHealth(int health)
    {
        slider.value = health;
    }
}
