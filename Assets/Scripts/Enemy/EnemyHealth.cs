using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Tooltip("Максимальное здоровье противника")]
    [SerializeField] private int _maxHealth = 1;      
    [Tooltip("Событие - получение урона противником")]
    public event Action<int, int> EnemyHealthChanged;
    [Tooltip("Событие - убийство противника")] 
    public event Action<EnemyHealth> EnemyKilled;
    // Текущее здоровье противника
    private int _health;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damageValue)
    {
        _health -= damageValue;
        if (_health > 0)
            ShowHealth();
        if (_health <= 0)
            Die();
    }

    public void ShowHealth()
    {
        EnemyHealthChanged.Invoke(_health, _maxHealth);
    }

    public void Die()
    {
        EnemyKilled?.Invoke(this);
    }
}
