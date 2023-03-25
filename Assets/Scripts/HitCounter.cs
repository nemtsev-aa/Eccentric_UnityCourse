using System;
using UnityEngine;

public class HitCounter : MonoBehaviour
{
    public static HitCounter Instance;
    [Tooltip("���������� ���������")]
    private int _hitCount;

    public event Action<int> OnHitRegistration;
    public event Action OnHit;

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
    /// ������� ���������� ��������� �� ������
    /// </summary>
    public void HitCounting(GameObject hitTarget)
    {
        _hitCount++;
        OnHitRegistration?.Invoke(_hitCount);
        if (hitTarget.TryGetComponent(out EnemyHealth enemyHealth))
        {
            if (enemyHealth.EnemyType == EnemyType.Bear)
            {
                OnHit?.Invoke();
            }   
        } 
    }

    public void ResetCounter()
    {
        _hitCount = 0;
    }
}
