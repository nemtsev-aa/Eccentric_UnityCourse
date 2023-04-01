using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Tooltip("������������ �������� ����������")]
    [SerializeField] private int _maxHealth = 1;      
    [Tooltip("������� - ��������� ����� �����������")]
    public event Action<int, int> EnemyHealthChanged;
    [Tooltip("������� - �������� ����������")] 
    public event Action<EnemyHealth> EnemyKilled;
    // ������� �������� ����������
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
