using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBlink : MonoBehaviour
{
    [SerializeField] private float blinkDecaySpeed = 1f;
    [SerializeField] private Color blinkColor = Color.white;

    [SerializeField] private SpriteRenderer[] spriteRenderers;
    private MaterialPropertyBlock _materialPropertyBlock;
    private float _blinkFactor;

    private void Start()
    {
        _materialPropertyBlock = new MaterialPropertyBlock();
    }

    private void Update()
    {
        if (_blinkFactor <= 0f)
        {
            return;
        }

        _blinkFactor = Mathf.Lerp(_blinkFactor, 0f, Time.deltaTime * blinkDecaySpeed);
        if (_blinkFactor < 0.01)
        {
            _blinkFactor = 0f;
        }
        ApplyBlinkFactor();
    }

    public void Blink(Color color)
    {
        _blinkFactor = 1f;
        blinkColor = color;
        ApplyBlinkFactor();
    }

    private void ApplyBlinkFactor()
    {
        foreach (var renderer in spriteRenderers)
        {
            renderer.GetPropertyBlock(_materialPropertyBlock);
            _materialPropertyBlock.SetColor("_BlinkColor", blinkColor);
            _materialPropertyBlock.SetFloat(name: "_BlinkFactor", _blinkFactor);
            renderer.SetPropertyBlock(_materialPropertyBlock);
        }
        
    }
}
