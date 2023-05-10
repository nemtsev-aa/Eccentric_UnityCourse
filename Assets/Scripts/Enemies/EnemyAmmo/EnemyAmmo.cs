using System;
using UnityEngine;

public enum EnemyAmmoType
{
    Rocket,
    Carrot,
    Acorn
}

public class EnemyAmmo : Enemy
{
    [Header("Ammo Settings")]
    [Tooltip("��� �����������")]
    [SerializeField] private EnemyAmmoType EnemyAmmoType;

    [Tooltip("������� - ��������� ���� ���������")]
    public event Action<EnemyAmmo> EnemyAmmoDestroy;

    private void OnEnable()
    {
        EnemyHealth.HealthIsOver += Die;
    }

    private void OnDisable()
    {
        EnemyHealth.HealthIsOver -= Die;
    }

    private void Die(EnemyHealth enemyHealth)
    {
        EnemyAmmoDestroy?.Invoke(this);
        // ������� ������ ����� �� �����
        Destroy(enemyHealth.gameObject);
    }
}
