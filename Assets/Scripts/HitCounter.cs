using System;
using UnityEngine;

public class HitCounter : MonoBehaviour
{
    public static HitCounter Instance;
    [Tooltip("Количество попаданий")]
    private int _hitCount;

    public event Action<int> OnHitRegistration;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Подсчёт количества попаданий во врагов
    /// </summary>
    public void HitCounting(GameObject hitTarget)
    {
        EnemyHealth enemyHealth = hitTarget.GetComponentInParent<EnemyHealth>();
        if (enemyHealth)
        {
            _hitCount++;
            OnHitRegistration?.Invoke(_hitCount);
        }
    }

    public void ResetCounter()
    {
        _hitCount = 0;
    }
}
