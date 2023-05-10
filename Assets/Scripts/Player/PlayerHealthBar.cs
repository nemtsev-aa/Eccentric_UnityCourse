using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [Tooltip("Индикатор здоровья персонажа")]
    [SerializeField] private PlayerHealth _playerHealth;
    [Tooltip("Индикатор здоровья персонажа")]
    [SerializeField] private Image _healthView;
    [Tooltip("Градиент здоровья персонажа")]
    [SerializeField] private Gradient _healthGradient;

    private void OnEnable()
    {
        _playerHealth.OnHealthChange += ShowHealth;
        _playerHealth.OnAddHealth += ShowHealth;
    }

    private void OnDisable()
    {
        _playerHealth.OnHealthChange -= ShowHealth;
        _playerHealth.OnAddHealth -= ShowHealth;
    }

    private void ShowHealth(float Health, float MaxHealth)
    {   
        float viewHealthValue = Health / MaxHealth;
        _healthView.fillAmount = viewHealthValue;
        _healthView.color = _healthGradient.Evaluate(viewHealthValue);
    }
}
