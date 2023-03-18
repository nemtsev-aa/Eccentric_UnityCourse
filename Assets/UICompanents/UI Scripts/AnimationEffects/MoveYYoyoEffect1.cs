using UnityEngine;
using DG.Tweening;

public class MoveYYoyoEffect : MonoBehaviour
{
    [Tooltip("��������")]
    [SerializeField] float _offset = -1000f;
    [Tooltip("�����������������")]
    [SerializeField] float duration = 10f;

    void Start()
    {
        transform.DOMoveY(_offset, duration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
