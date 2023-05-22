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
    public UnitType UnitType;
    public int Price;
    public int Health; 
    public NavMeshAgent NavMeshAgent;
    public ParticleSystem DamageEffect;
    public GameObject HealthBarPrefab;

    [Space(10)]
    [Header("SoundsEffect")]
    [SerializeField] private AudioClip _selectionSound;
    [Space(10)]
    [Header("Animation")]
    [SerializeField] private Animator _animator;

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
        NavMeshAgent.SetDestination(point);
    }

    private void Update() {
        if (NavMeshAgent.velocity.magnitude > 0) {
            _animator.SetTrigger("Run");
            _animator.SetFloat("MoveSpeed", NavMeshAgent.velocity.magnitude);
        } else {
            _animator.SetTrigger("Idle");
        }
    }

    public void TakeDamage(int damageValue) {
        Health -= damageValue;
        _healthBar.SetHealth(Health, _maxHealth);
        ParticleSystem damageEffect = Instantiate(DamageEffect, transform.position, Quaternion.identity);
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
