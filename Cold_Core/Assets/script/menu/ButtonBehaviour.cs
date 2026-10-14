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
    private Vector3 _scale;

    private void OnEnable()
    {
        _image = GetComponent<Image>();
        _scale = transform.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOKill();
        if (_hoverSprite == null)
        {
            transform.DOScale(new Vector3(_scale.x + 0.2f, _scale.y + 0.2f, _scale.z + 0.2f), 0.5f).SetUpdate(true);
            Debug.Log("bigger");
        }
        else
        {
            _image.sprite = _hoverSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOKill();
        if (_idleSprite == null)
        {
            transform.DOScale(_scale, 0.5f).SetUpdate(true);
            Debug.Log("smaller");
        }
        else
        {
            _image.sprite = _idleSprite;
        }


    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.DOKill();
        if (_pressedSprite == null)
        {

        }
        else
        {
            _image.sprite = _pressedSprite;
        }
            
    }
}
