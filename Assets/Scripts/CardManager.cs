using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [Tooltip("������������ ������ ��� ����")]
    [SerializeField] private GameObject _cardManagerParent;
    [Tooltip("������ ����")]
    [SerializeField] private Card[] _effectCards;

    public void ShowCards(List<Effect> effects)
    {
        _cardManagerParent.SetActive(true); // ���������� ������ ����
        for (int i = 0; i < effects.Count; i++) 
        {
            _effectCards[i].Show(effects[i]); // ��������� ������ ����� ���������������� ����������
        }
    }
}
