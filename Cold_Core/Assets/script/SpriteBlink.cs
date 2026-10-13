using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBlink : MonoBehaviour
{
    [SerializeField] private float blinkDecaySpeed = 1f;

    private SpriteRenderer _spriteRenderer;
    private MaterialPropertyBlock _materialPropertyBlock;
    private float _blinkFactor;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
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

    public void Blink()
    {
        _blinkFactor = 1f;
        ApplyBlinkFactor();
    }

    private void ApplyBlinkFactor()
    {
        _spriteRenderer.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetFloat(name: "_BlinkFactor", _blinkFactor);
        _spriteRenderer.SetPropertyBlock(_materialPropertyBlock);
    }
}
