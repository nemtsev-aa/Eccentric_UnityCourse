using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum UnitType {
    Knight,
    Farmer,
    Builder,
    Scout
}
public class Unit : SelectableObject
{
    [Tooltip("Тип юнита")]
    public UnitType UnitType;
    [Tooltip("Текущее состояние")]
    public UnitState CurrentUnitState;
    
    [Header("Parametrs")]
    [Tooltip("Цена")]
    public int Price;
    [Tooltip("Количество здоровья")]
    public int Health;
    [Tooltip("ДПС")]
    public int DamagePerSecond;
    [Tooltip("Период атаки")]
    public float AttackPeriod = 0.1f;
    [Tooltip("Радиус видимости")]
    public float DistanceToFollow = 7f;
    [Tooltip("Радиус атаки")]
    public float DistanceToAttack = 1f;
    [Tooltip("Полоска жизни")]
    public GameObject HealthBarPrefab;

    [Space(10)]
    [Header("FX Effects")]
    [SerializeField] private AudioClip _selectionSound;
    [SerializeField] private ParticleSystem _damageEffect;
    [Space(10)]
    [SerializeField] protected Animator _animator;
    [SerializeField] protected NavMeshAgent _navMeshAgent;

    [Header("Targets")]
    [Tooltip("Цель - точка")]
    public Building TargetPoint;
    [Tooltip("Цель - враг")]
    public Enemy TargetEnemy;

    private int _maxHealth;
    private AudioSource _audioSource;
    private HealthBar _healthBar;
    
    public override void Start() {
        base.Start();
        _maxHealth = Health;
        GameObject healthBar = Instantiate(HealthBarPrefab);
        healthBar.transform.parent = transform;
        _healthBar = healthBar.GetComponent<HealthBar>();
        _healthBar.Setup(transform);

        _audioSource = GetComponent<AudioSource>();
    }

    public override void Select() {
        base.Select();
        _audioSource.clip = _selectionSound;
        _audioSource.Play();
    }

    public override void WhenClickOnGround(Vector3 point) {
        base.WhenClickOnGround(point);
        _navMeshAgent.SetDestination(point);
    }

    public void TakeDamage(int damageValue) {
        Health -= damageValue;
        _healthBar.SetHealth(Health, _maxHealth);
        ParticleSystem damageEffect = Instantiate(_damageEffect, transform.position, Quaternion.identity);
        Destroy(damageEffect.gameObject, 1f);
        _animator.SetTrigger("TakeDamage");
        if (Health <= 0) {
            Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }

    private void OnDestroy() {
        Destroy(_healthBar.gameObject);
    }
}
