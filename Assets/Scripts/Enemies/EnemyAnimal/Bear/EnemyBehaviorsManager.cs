using Assets.Scripts.Enemy.Bear;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorsManager : MonoBehaviour
{
    [Tooltip("Аниматор")]
    public Animator Animator;
    [Tooltip("Дальность атаки")]
    public float AttackRange = 70f;
    [Tooltip("Период атаки")]
    public float AttackPeriod = 0.1f;
    [Tooltip("Период неуязвимости")]
    public float InvulnerablePeriod = 1f;
    public bool IsInvulnerable;
    public BlinkEffect Blink;
    public EnemyHealth EnemyHealth;
    // Карта состояний объекта
    private Dictionary<Type, IEnemyBehavior> _behaviorsMap;
    // Текущее состояние
    private IEnemyBehavior _behaviorCurrent;
    // Положение игрока
    public Transform PlayerTransform;
    // Якорь для щита
    public Transform AncorForShield;
    // Щит
    public GameObject Shield;

    // Время с прошлого переключения атаки
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
            if (IsInvulnerable)
                _behaviorCurrent.Play();
            else
            {
                _timer += Time.deltaTime;
                if (_timer >= AttackPeriod)
                {
                    DistanceToPlayer();
                    _timer = 0;
                }
            }
        }
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
        _behaviorsMap[typeof(EnemyBehaviorInvulnerable)] = new EnemyBehaviorInvulnerable(this);
    }

    private void OnEnable()
    {
        EnemyHealth.HealthDecreased += SetBehaviorTakeDamage;
    }

    private void OnDisable()
    {
        EnemyHealth.HealthDecreased -= SetBehaviorTakeDamage;
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
    public void SetBehaviorTakeDamage(int currentHealth, int maxHealth)
    {
        if (!IsInvulnerable)
        {
            SetBehaviorDistanceAttack();
            var newBehavior = GetBehavior<EnemyBehaviorTakeDamage>();
            SetBehavior(newBehavior);
           
            SetInvulnerable();
        }
    }

    /// <summary>
    /// Переключение в режим "Неуязвимость"
    /// </summary>
    public void SetInvulnerable()
    {
        var newBehavior = GetBehavior<EnemyBehaviorInvulnerable>();
        SetBehavior(newBehavior);
    }

    /// <summary>
    /// Временная неуязвимость
    /// </summary>
    public void SetTemporaryInvulnerability(bool invulnerabilityStatus, bool moveStatus)
    {
        gameObject.GetComponent<LeftToRightMove>().enabled = moveStatus;
    }

    public void DistanceToPlayer()
    {
        // Вектор от игрока до врага
        if (PlayerTransform != null)
        {
            // Модуль вектора
            float currentDistanceValue = Vector3.Distance(PlayerTransform.position,transform.position);
            Debug.Log(currentDistanceValue);
            if (currentDistanceValue < 5f)
            {
                if (_behaviorCurrent != GetBehavior<EnemyBehaviorMeleeAttack>())
                    SetBehaviorMeleeAttack();
                else
                    _behaviorCurrent.Enter();

            }
            else if (currentDistanceValue >= 6f && currentDistanceValue < 20f)
            {
                if (_behaviorCurrent != GetBehavior<EnemyBehaviorDistanceAttack>())
                    SetBehaviorDistanceAttack();
                else
                    _behaviorCurrent.Enter();
            }
            //else
            //{
            //    SetBehaviorIdle();
            //}
        } 
    }
}
