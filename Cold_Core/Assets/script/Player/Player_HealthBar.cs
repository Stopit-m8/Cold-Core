using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player_HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;


    private Tween healthTween;
    private Tween backTween;

    [SerializeField] private Slider sliderBack;
    [SerializeField] private float backDelay;

    [SerializeField] private float healthDecreaseDuration;
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
        sliderBack.maxValue = health;
        sliderBack.value = health;
    }
    public void setHealth(int health)
    {
        healthTween?.Kill();
        backTween?.Kill();

        healthTween = slider
            .DOValue(health, healthDecreaseDuration)
            .SetEase(Ease.OutQuad);

        backTween = DOTween.Sequence()
            .AppendInterval(backDelay)
            .Append(sliderBack.DOValue(health, healthDecreaseDuration)
                .SetEase(Ease.OutQuad));
    }

    IEnumerator StartSetHealth(int health)
    {
        slider.DOValue(health, healthDecreaseDuration).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(0.5f);
        sliderBack.DOValue(health, healthDecreaseDuration).SetEase(Ease.OutQuad);
    }
}
