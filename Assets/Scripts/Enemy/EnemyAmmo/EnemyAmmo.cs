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
    [Tooltip("Тип боеприпасов")]
    [SerializeField] private EnemyAmmoType EnemyAmmoType;

    [Tooltip("Событие - боеприпас вгра уничтожен")]
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
        // Удаляем объект врага со сцены
        Destroy(enemyHealth.gameObject);
    }
}
