using System;
using UnityEngine;

public class HitCounter : MonoBehaviour
{
    public static HitCounter Instance;
    [Tooltip("���������� ���������")]
    private int _hitCount;

    public event Action<int> OnHitRegistration;
    public event Action<int> OnHitBearRegistration;

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
    public void HitCounting(GameObject hitTarget, Bullet bullet)
    {
        EnemyHealth enemyHealth = hitTarget.GetComponentInParent<EnemyHealth>();
        if (enemyHealth)
        {
            // �������� ���������: �������� ����� �� ���� * ���������� ���������
            int hitValue = bullet.DamageValue * bullet.GetRicochetCount();
            Debug.Log("RocketHit: " + hitValue);
            // ����� ���������� ���������
            _hitCount += hitValue;
            OnHitRegistration?.Invoke(_hitCount);

            //if (enemyHealth.EnemyType == EnemyType.Bear)
            //{
            //    OnHitBearRegistration?.Invoke(hitValue);
            //}
        }
    }

    public void ResetCounter()
    {
        _hitCount = 0;
    }
}
