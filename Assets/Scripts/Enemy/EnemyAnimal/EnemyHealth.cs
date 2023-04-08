using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Tooltip("������������ �������� ����������")]
    [SerializeField] private int _maxHealth = 1;      
    [Tooltip("������� - ���������� �������� ��������� �����������")]
    public event Action<int, int> HealthDecreased;
    [Tooltip("������� - �������� ��������� �����������")] 
    public event Action<EnemyHealth> HealthIsOver;
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
        HealthDecreased.Invoke(_health, _maxHealth);
    }

    public void Die()
    {
        HealthIsOver?.Invoke(this);
    }
}
