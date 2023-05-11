using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    [Tooltip("Родительский объект для карт")]
    [SerializeField] private GameObject _cardManagerParent;
    [Tooltip("Массив карт")]
    [SerializeField] private Card[] _effectCards;
    [SerializeField] private EffectsManager _effectsManager;
    [SerializeField] private GameStateManager _gameStateManager;

    private void Awake()
    {
        for (int i = 0; i < _effectCards.Length; i++)
        {
            _effectCards[i].Init(_effectsManager, this);
        }
    }

    public void ShowCards(List<Effect> effects)
    {
        _cardManagerParent.SetActive(true); // Активируем группу карт
        for (int i = 0; i < effects.Count; i++) 
        {
            _effectCards[i].Show(effects[i]); // Заполняем каждую карту соответствующими значениями
        }
        _gameStateManager.SetCards();
    }

    public void Hide()
    {
        _cardManagerParent.SetActive(false);
        _gameStateManager.SetAction();
    }
}
