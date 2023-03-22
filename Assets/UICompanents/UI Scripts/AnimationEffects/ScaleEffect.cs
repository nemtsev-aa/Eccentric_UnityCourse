using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ScaleEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("Масштаб по умолчанию")]
    [SerializeField] private Vector3 _defaultScale = Vector3.one;
    [Tooltip("Отклонение масштаба")]
    [SerializeField] private float _offsetScale = 0.95f;
    [Tooltip("Продолжительность")]
    [SerializeField] private float _duration = 0.3f;

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnScale();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OffScale();
    }

    private void OnScale()
    {
       transform.DOScale(Vector3.one * _offsetScale, _duration).SetEase(Ease.Linear).SetUpdate(true);
    }

    private void OffScale()
    {
       transform.DOScale(_defaultScale.x, _duration).SetEase(Ease.Linear).SetUpdate(true);
    }


}
