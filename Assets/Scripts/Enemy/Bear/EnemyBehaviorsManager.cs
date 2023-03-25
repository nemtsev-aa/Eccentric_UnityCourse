using Assets.Scripts.Enemy.Bear;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorsManager : MonoBehaviour
{
    [Tooltip("Аниматор")]
    public Animator Animator;

    [Tooltip("Дальность атаки")]
    [field: SerializeField] public float AttackRange { get; } = 70f;
    [Tooltip("Период атаки")]
    [field: SerializeField] public float AttackPeriod { get; } = 2f;

    public BlinkEffect Blink;

    private EnemyHealth _enemyHealth;

    public EnemyHealth EnemyHealth { get { return _enemyHealth; } private set {_enemyHealth = value;} }

    // Карта состояний объекта
    private Dictionary<Type, IEnemyBehavior> _behaviorsMap;
    // Текущее состояние
    private IEnemyBehavior _behaviorCurrent;
    // Положение игрока
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
    /// Инициализация карты состояний объекта
    /// </summary>
    private void InitBehaviors()
    {
        // Инициализируем карту состояний объекта в виде словаря
        _behaviorsMap = new Dictionary<Type, IEnemyBehavior>();
        // В качестве ключей словаря используем классы состояний
        // В качестве элементов словаря используем экземпляры классов состояний
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
    /// Устанавливка состояния объекта
    /// </summary>
    private void SetBehavior(IEnemyBehavior newBehavior)
    {
        if (_behaviorCurrent != null)
            _behaviorCurrent.Exit();

        _behaviorCurrent = newBehavior;
        _behaviorCurrent.Enter();
    }

    /// <summary>
    /// Устанавливка состояния объекта по-умолчанию
    /// </summary>
    private void SetBehaviorByDefault()
    {
        SetBehaviorIdle();
    }

    /// <summary>
    /// Получения состояния объекта из карты состояний
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
    /// Переключение в режим "Покой"
    /// </summary>
    public void SetBehaviorIdle()
    {
        var newBehavior = GetBehavior<EnemyBehaviorIdle>();
        SetBehavior(newBehavior);
    }

    /// <summary>
    /// Переключение в режим "Дистанционная атака"
    /// </summary>
    public void SetBehaviorDistanceAttack()
    {
        var newBehavior = GetBehavior<EnemyBehaviorDistanceAttack>();
        SetBehavior(newBehavior);
    }

    /// <summary>
    /// Переключение в режим "Ближняя атака"
    /// </summary>
    public void SetBehaviorMeleeAttack()
    {
        var newBehavior = GetBehavior<EnemyBehaviorMeleeAttack>();
        SetBehavior(newBehavior);
    }

    /// <summary>
    /// Переключение в режим "Получение урона"
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
        // Вектор от игрока до врага
        Vector3 currentDistance = PlayerTransform.position - transform.position;
        // Модуль вектора
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
