using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    [Tooltip("������������ ������ ��� ����")]
    [SerializeField] private GameObject _cardManagerParent;
    [Tooltip("������ ����")]
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
        _cardManagerParent.SetActive(true); // ���������� ������ ����
        for (int i = 0; i < effects.Count; i++) 
        {
            _effectCards[i].Show(effects[i]); // ��������� ������ ����� ���������������� ����������
        }
        _gameStateManager.SetCards();
    }

    public void Hide()
    {
        _cardManagerParent.SetActive(false);
        _gameStateManager.SetAction();
    }
}
