using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour, IPointerEnterHandler
{
    [Tooltip("Отклонение затухания")]
    [SerializeField] private float _offsetFade = 0.5f;
    [Tooltip("Продолжительность")]
    [SerializeField] private float _duration = 0.3f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Fade();
    }

    private void Fade()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(gameObject.GetComponent<Image>().DOFade(_offsetFade, _duration));
        mySequence.Append(gameObject.GetComponent<Image>().material.DOFade(1f, _duration));
    }
}
