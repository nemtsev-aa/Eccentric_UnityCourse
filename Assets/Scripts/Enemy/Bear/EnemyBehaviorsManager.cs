using Assets.Scripts.Enemy.Bear;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorsManager : MonoBehaviour
{
    [Tooltip("��������")]
    public Animator Animator;
    [Tooltip("��������� �����")]
    public float AttackRange = 70f;
    [Tooltip("������ �����")]
    public float AttackPeriod = 2f;
    public BlinkEffect Blink;
    public EnemyHealth EnemyHealth;
    // ����� ��������� �������
    private Dictionary<Type, IEnemyBehavior> _behaviorsMap;
    // ������� ���������
    private IEnemyBehavior _behaviorCurrent;
    // ��������� ������
    public Transform PlayerTransform;

    // ����� � �������� ������������
    private float _timer;

    private void Start()
    {
        InitBehaviors();
        SetBehaviorByDefault();
    }

    public void Update()
    {
        if (_behaviorCurrent != null)
        {
            _behaviorCurrent.Play();
            
            _timer += Time.deltaTime;
            if (_timer >= AttackPeriod)
            {
                DistanceToPlayer();
            } 
        }
    }
    /// <summary>
    /// ������������� ����� ��������� �������
    /// </summary>
    private void InitBehaviors()
    {
        // �������������� ����� ��������� ������� � ���� �������
        _behaviorsMap = new Dictionary<Type, IEnemyBehavior>();
        // � �������� ������ ������� ���������� ������ ���������
        // � �������� ��������� ������� ���������� ���������� ������� ���������
        _behaviorsMap[typeof(EnemyBehaviorIdle)] = new EnemyBehaviorIdle(this);
        _behaviorsMap[typeof(EnemyBehaviorDistanceAttack)] = new EnemyBehaviorDistanceAttack(this);
        _behaviorsMap[typeof(EnemyBehaviorMeleeAttack)] = new EnemyBehaviorMeleeAttack(this);
        _behaviorsMap[typeof(EnemyBehaviorTakeDamage)] = new EnemyBehaviorTakeDamage(this);
        _behaviorsMap[typeof(EnemyBehaviorInvulnerable)] = new EnemyBehaviorInvulnerable(this);
    }

    private void OnEnable()
    {
        if (HitCounter.Instance != null)
        {
            HitCounter.Instance.OnHitRegistration += SetBehaviorTakeDamage;
        }
    }

    private void OnDisable()
    {
        HitCounter.Instance.OnHitRegistration -= SetBehaviorTakeDamage;
    }

    /// <summary>
    /// ������������ ��������� �������
    /// </summary>
    private void SetBehavior(IEnemyBehavior newBehavior)
    {
        if (_behaviorCurrent != null)
            _behaviorCurrent.Exit();

        _behaviorCurrent = newBehavior;
        _behaviorCurrent.Enter();
    }

    /// <summary>
    /// ������������ ��������� ������� ��-���������
    /// </summary>
    private void SetBehaviorByDefault()
    {
        SetBehaviorIdle();
    }

    /// <summary>
    /// ��������� ��������� ������� �� ����� ���������
    /// </summary>
    private IEnemyBehavior GetBehavior<T>() where T : IEnemyBehavior
    {
        var type = typeof(T);
        return _behaviorsMap[type];
    }

    /// <summary>
    /// ������������ � ����� "�����"
    /// </summary>
    public void SetBehaviorIdle()
    {
        var newBehavior = GetBehavior<EnemyBehaviorIdle>();
        SetBehavior(newBehavior);
    }

    /// <summary>
    /// ������������ � ����� "������������� �����"
    /// </summary>
    public void SetBehaviorDistanceAttack()
    {
        var newBehavior = GetBehavior<EnemyBehaviorDistanceAttack>();
        SetBehavior(newBehavior);
    }

    /// <summary>
    /// ������������ � ����� "������� �����"
    /// </summary>
    public void SetBehaviorMeleeAttack()
    {
        var newBehavior = GetBehavior<EnemyBehaviorMeleeAttack>();
        SetBehavior(newBehavior);
    }

    /// <summary>
    /// ������������ � ����� "��������� �����"
    /// </summary>
    public void SetBehaviorTakeDamage(int damageValue)
    {
        if (!EnemyHealth.Invulnerable)
        {
            Debug.Log("Hit");
            var newBehavior = GetBehavior<EnemyBehaviorTakeDamage>();
            SetBehavior(newBehavior);
        }
    }

    /// <summary>
    /// ������������ � ����� "������������"
    /// </summary>
    public void SetInvulnerable()
    {
        var newBehavior = GetBehavior<EnemyBehaviorInvulnerable>();
        SetBehavior(newBehavior);
    }

    /// <summary>
    /// ��������� ������������
    /// </summary>
    public void SetTemporaryInvulnerability(bool invulnerabilityStatus, bool moveStatus)
    {
        EnemyHealth.Invulnerable = invulnerabilityStatus;
        gameObject.GetComponent<LeftToRightMove>().enabled = moveStatus;
    }

    public void DistanceToPlayer()
    {
        // ������ �� ������ �� �����
        if (PlayerTransform != null)
        {
            Vector3 currentDistance = PlayerTransform.position - transform.position;
            // ������ �������
            float currentDistanceValue = currentDistance.sqrMagnitude;

            if (currentDistanceValue <= 20f)
            {
                if (_behaviorCurrent != GetBehavior<EnemyBehaviorMeleeAttack>())
                    SetBehaviorMeleeAttack();
            }
            else if (currentDistanceValue >= 20f && currentDistanceValue < 100f)
            {
                if (_behaviorCurrent != GetBehavior<EnemyBehaviorDistanceAttack>())
                    SetBehaviorDistanceAttack();
            }
            else
            {
                SetBehaviorIdle();
            }
        } 
    }
}
