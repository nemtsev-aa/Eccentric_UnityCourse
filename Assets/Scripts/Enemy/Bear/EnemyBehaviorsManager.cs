using Assets.Scripts.Enemy.Bear;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorsManager : MonoBehaviour
{
    [Tooltip("��������")]
    public Animator Animator;

    [Tooltip("��������� �����")]
    [field: SerializeField] public float AttackRange { get; } = 70f;
    [Tooltip("������ �����")]
    [field: SerializeField] public float AttackPeriod { get; } = 2f;

    public BlinkEffect Blink;

    private EnemyHealth _enemyHealth;

    public EnemyHealth EnemyHealth { get { return _enemyHealth; } private set {_enemyHealth = value;} }

    // ����� ��������� �������
    private Dictionary<Type, IEnemyBehavior> _behaviorsMap;
    // ������� ���������
    private IEnemyBehavior _behaviorCurrent;
    // ��������� ������
    private Transform _playerTransform;
    public Transform PlayerTransform { get { return _playerTransform; } }

    private void Start()
    {
        InitBehaviors();
        SetBehaviorByDefault();

        _playerTransform = FindObjectOfType<PlayerMove>().transform;
        EnemyHealth = gameObject.GetComponent<EnemyHealth>();
        Debug.Log(EnemyHealth.EnemyType);
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
    }

    private void OnEnable()
    {
        if (HitCounter.Instance != null)
        {
            HitCounter.Instance.OnHit += SetBehaviorTakeDamage;
        }
    }

    private void OnDisable()
    {
        HitCounter.Instance.OnHit -= SetBehaviorTakeDamage;
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

    private void Update()
    {
        if (_behaviorCurrent != null)
        {
            _behaviorCurrent.Play();
            DistanceToPlayer();
        }
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
    public void SetBehaviorTakeDamage()
    {
        var newBehavior = GetBehavior<EnemyBehaviorTakeDamage>();
        if (!_enemyHealth.GetInvulnerable())
        {
            SetBehavior(newBehavior);
        }
    }

    private void DistanceToPlayer()
    {
        // ������ �� ������ �� �����
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
