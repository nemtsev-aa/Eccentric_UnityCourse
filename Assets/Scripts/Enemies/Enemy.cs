using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState {
    Idle,
    UnitAttack,
    BuildingAttack,
    WalkToBuilding,
    WalkToUnit
}
public class Enemy : MonoBehaviour {
    [Tooltip("������� ��������� �����")]
    public EnemyState CurrentEnemyState;
    [Header("Parametrs")]
    [Tooltip("��������")]
    public int Health;
    [Tooltip("���")]
    public int DamagePerSecond;
    [Tooltip("������ �����")]
    public float AttackPeriod = 0.1f;
    [Tooltip("������ ��������� ������")]
    public float DistanceToFollow = 7f;
    [Tooltip("������ �����")]
    public float DistanceToAttack = 1f;
    public ParticleSystem DamageEffect;
    public GameObject HealthBarPrefab;

    [Header("Targets")]
    [Tooltip("���� - ������")]
    public Building TargetBuilding;
    public LayerMask _layerMaskBuildings;
    [Tooltip("���� - ����")]
    public Unit TargetUnit;
    public LayerMask _layerMaskUnits;

    [Tooltip("��������")]
    [SerializeField] private Animator _animator;
    [Tooltip("�� �����������")]
    [SerializeField] private NavMeshAgent _agent;

    [Tooltip("������� - ���� ���������")]
    public event Action<Enemy> EnemyKilled;

    private Transform _targetPosition; // ������� ������ ������
    private int _maxHealth;
    private HealthBar _healthBar;
    private float _timer;

    public void Init(Building targetBuilding) {
        TargetBuilding = targetBuilding;
    }

    private void Start() {
        _maxHealth = Health;
        GameObject healthBar = Instantiate(HealthBarPrefab);
        healthBar.transform.parent = transform;
        _healthBar = healthBar.GetComponent<HealthBar>();
        _healthBar.Setup(transform);

        SetState(EnemyState.WalkToBuilding);
    }

    private void Update() {

        switch (CurrentEnemyState) {
            case EnemyState.Idle:
                if (TargetUnit == null) {
                    FindClosestUnit();
                } else if (TargetBuilding == null) {
                    FindClosestBuilding();
                } else {
                    _animator.SetTrigger("Idle");
                    _animator.SetFloat("MoveSpeed", 0);
                }
                FindClosestUnit();
                break;
            case EnemyState.WalkToBuilding:
                FindClosestBuilding();
                if (TargetBuilding == null) {
                    FindClosestUnit();
                } else {
                    _agent.SetDestination(TargetBuilding.transform.position);
                    float distanceToBuilding = Vector3.Distance(transform.position, TargetBuilding.transform.position);
                    if (distanceToBuilding < DistanceToAttack) SetState(EnemyState.BuildingAttack);
                }

                if (_agent.velocity.magnitude > 0) {
                    _animator.SetTrigger("Run");
                    _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
                } else {
                    _animator.SetTrigger("Idle");
                }

                break;
            case EnemyState.WalkToUnit:
                if (TargetUnit) {
                    _agent.SetDestination(TargetUnit.transform.position);
                    float distanceToUnit = Vector3.Distance(transform.position, TargetUnit.transform.position);
                    if (distanceToUnit > DistanceToFollow) SetState(EnemyState.WalkToBuilding);
                    if (distanceToUnit < DistanceToAttack) SetState(EnemyState.UnitAttack);

                    if (_agent.velocity.magnitude > 0) {
                        _animator.SetTrigger("Run");
                        _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
                    }
                } else {
                    SetState(EnemyState.Idle);
                }
                break;
            case EnemyState.UnitAttack:
                if (TargetUnit) {
                    _agent.SetDestination(TargetUnit.transform.position);
                    float distanceToTarget = Vector3.Distance(transform.position, TargetUnit.transform.position);
                    if (distanceToTarget < DistanceToAttack) {
                        _animator.SetTrigger("Attack");
                        _timer += Time.deltaTime;
                        if (_timer > AttackPeriod) {
                            _timer = 0;
                            TargetUnit.TakeDamage(DamagePerSecond);
                        }
                    } else {
                        SetState(EnemyState.WalkToUnit);
                    }
                } else {
                    SetState(EnemyState.Idle);
                }
                break;
            case EnemyState.BuildingAttack:
                if (TargetBuilding) {
                    _agent.SetDestination(TargetBuilding.transform.position + new Vector3(1f, 0f, 1f));
                    float distanceToBuilding = Vector3.Distance(transform.position, TargetBuilding.transform.position);
                    if (distanceToBuilding < DistanceToAttack) {
                        _animator.SetTrigger("Attack");
                        _timer += Time.deltaTime;
                        if (_timer > AttackPeriod) {
                            _timer = 0;
                            TargetBuilding.TakeDamage(DamagePerSecond);
                        }
                    } else {
                        SetState(EnemyState.WalkToBuilding);
                    }
                } else {
                    SetState(EnemyState.Idle);
                }
                break;
            default:
                break;
        }
    }

    public void SetState(EnemyState enemyState) {
        CurrentEnemyState = enemyState;
        switch (CurrentEnemyState) {
            case EnemyState.Idle:
                _animator.SetTrigger("Idle");
                break;
            case EnemyState.WalkToBuilding:
                if (TargetBuilding) {
                    _agent.SetDestination(TargetBuilding.transform.position);
                }
                break;
            case EnemyState.WalkToUnit:
                if (TargetUnit) {
                    _agent.SetDestination(TargetUnit.transform.position);
                } else {
                    FindClosestUnit();
                }
                break;
            case EnemyState.UnitAttack:
                _timer = 0f;
                break;
            case EnemyState.BuildingAttack:
                _timer = 0f;
                break;
            default:
                break;
        }
    }

    private void FindClosestBuilding() {
        if (TargetBuilding == null) {
            
            Collider[] allColliders = Physics.OverlapSphere(transform.position, DistanceToFollow * 2, _layerMaskBuildings);           
            float minDistance = Mathf.Infinity; // ���������� �� ���������� ������ - �������������
            Building closestBuilding = null; // ��������� ������ �� �������

            for (int i = 0; i < allColliders.Length; i++) {
                Transform iParent = allColliders[i].gameObject.transform;
                Building iBuilding = iParent.GetComponent<Building>();
                if (iBuilding) {
                    float distance = Vector3.Distance(transform.position, iBuilding.transform.position); // ���������� �� i-�� ������
                    if (distance < minDistance) {
                        minDistance = distance;
                        closestBuilding = iBuilding;
                    }
                }
            }
            if (closestBuilding != null) {
                TargetBuilding = closestBuilding; // ��������� ������
                SetState(EnemyState.WalkToBuilding);
            }
        }
    }

    private void FindClosestUnit() {
        if (TargetUnit == null) {
            
            Collider[] allColliders = Physics.OverlapSphere(transform.position, DistanceToFollow, _layerMaskUnits);
            float minDistance = Mathf.Infinity; // ���������� �� ���������� ����� - �������������
            Unit closestUnit = null; // ��������� ���� �� ������
           
            for (int i = 0; i < allColliders.Length; i++) {
                Transform iParent = allColliders[i].gameObject.transform.parent;
                Unit iUnit = iParent.GetComponent<Unit>();
                if (iUnit) {
                    float distance = Vector3.Distance(transform.position, iUnit.transform.position); // ���������� �� i-�� �����
                    if (distance < minDistance) {
                        minDistance = distance;
                        closestUnit = iUnit;
                    }
                }
            }

            if (minDistance < DistanceToFollow && closestUnit != null) {
                TargetUnit = closestUnit; // ��������� ����
                SetState(EnemyState.WalkToUnit);
            } else {
                SetState(EnemyState.Idle);
            }
        }    
    }

    public void TakeDamage(int damageValue) {
        Health -= damageValue;
        _healthBar.SetHealth(Health, _maxHealth);
        ParticleSystem damageEffect = Instantiate(DamageEffect, transform.position, Quaternion.identity);
        Destroy(damageEffect.gameObject, 0.5f);
        _animator.SetTrigger("TakeDamage");
        if (Health <= 0) {
            Die();
        }
    }

    public void Die() {
        EnemyKilled?.Invoke(this);
        _animator.SetTrigger("Die");
        Destroy(gameObject, 2f);
    }

    private void OnDestroy() {
        if (_healthBar) {
            Destroy(_healthBar.gameObject);
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, DistanceToAttack);
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.up, DistanceToFollow);
    }
#endif
}
