using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Tooltip("ћаксимальное здоровье противника")]
    [SerializeField] private int _maxHealth = 1;      
    [Tooltip("—обытие - количество здоровь€ протиника уменьшилось")]
    public event Action<int, int> HealthDecreased;
    [Tooltip("—обытие - здоровье протиника закончилось")] 
    public event Action<EnemyHealth> HealthIsOver;
    // “екущее здоровье противника
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
        HealthDecreased.Invoke(_health, _maxHealth);
    }

    public void Die()
    {
        HealthIsOver?.Invoke(this);
    }
}
