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

    [Space(10)]
    [Header("SoundsEffect")]
    [SerializeField] private AudioClip _selectionSound;
    [Space(10)]
    [Header("Animation")]
    [SerializeField] private Animator _animator;

    private AudioSource _audioSource;

    private void Start() {
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
}
