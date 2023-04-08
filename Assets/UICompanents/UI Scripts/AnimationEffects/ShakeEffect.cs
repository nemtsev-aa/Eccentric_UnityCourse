using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ShakeEffect : MonoBehaviour, IPointerEnterHandler
{
    [Tooltip("Интенсивность")]
    [SerializeField] float strength = 0.2f;
    [Tooltip("Продолжительность")]
    [SerializeField] float duration = 0.5f;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Shake();
    }

    private void Shake()
    {
        transform.DOShakeScale(duration, strength).SetEase(Ease.OutElastic);
        transform.DOScale(Vector3.one, 0.1f);
    }
}
