using UnityEngine;
using DG.Tweening;

public class MoveXYoyoEffect : MonoBehaviour
{
    [Tooltip("Смещение")]
    [SerializeField] float _offset = -1000f;
    [Tooltip("Продолжительность")]
    [SerializeField] float duration = 10f;

    void Start()
    {
        transform.DOMoveX(_offset, duration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
