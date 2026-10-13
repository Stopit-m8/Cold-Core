using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Sprite _idleSprite;
    [SerializeField] private Sprite _hoverSprite;
    [SerializeField] private Sprite _pressedSprite;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        DOTween.KillAll();
        if (_hoverSprite == null)
        {
            transform.DOScale(1.2f, 0.5f);
        }
        else
        {
            _image.sprite = _hoverSprite;
        }
            
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DOTween.KillAll();
        if (_idleSprite == null)
        {
            transform.DOScale(1f, 0.5f);
        }
        else
        {
            _image.sprite = _idleSprite;
        }
            
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DOTween.KillAll();
        if (_pressedSprite == null)
        {

        }
        else
        {
            _image.sprite = _pressedSprite;
        }
            
    }
}
