using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [Tooltip("Родительский объект для карт")]
    [SerializeField] private GameObject _cardManagerParent;
    [Tooltip("Массив карт")]
    [SerializeField] private Card[] _effectCards;

    public void ShowCards(List<Effect> effects)
    {
        _cardManagerParent.SetActive(true); // Активируем группу карт
        for (int i = 0; i < effects.Count; i++) 
        {
            _effectCards[i].Show(effects[i]); // Заполняем каждую карту соответствующими значениями
        }
    }
}
