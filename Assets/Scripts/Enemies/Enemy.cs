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
    [Tooltip("Текущее состояние врага")]
    public EnemyState CurrentEnemyState;
    [Header("Parametrs")]
    [Tooltip("Здоровье")]
    public int Health;
    [Tooltip("ДПС")]
    public int DamagePerSecond;
    [Tooltip("Период атаки")]
    public float AttackPeriod = 0.1f;
    [Tooltip("Радиус видимости юнитов")]
    public float DistanceToFollow = 7f;
    [Tooltip("Радиус атаки")]
    public float DistanceToAttack = 1f;
    public ParticleSystem DamageEffect;
    public GameObject HealthBarPrefab;

    [Header("Targets")]
    [Tooltip("Цель - здание")]
    public Building TargetBuilding;
    [Tooltip("Цель - юнит")]
    public Unit TargetUnit;

    [Tooltip("Аниматор")]
    [SerializeField] private Animator _animator;
    [Tooltip("ИИ перемещения")]
    [SerializeField] private NavMeshAgent _agent;


    private int _maxHealth;
    private HealthBar _healthBar;
    private float _timer;

    private void Start() {
        _maxHealth = Health;
        GameObject healthBar = Instantiate(HealthBarPrefab);
        healthBar.transform.parent = transform;
        _healthBar = healthBar.GetComponent<HealthBar>();
        _healthBar.Setup(transform);

        //_audioSource = GetComponent<AudioSource>();

        SetState(EnemyState.WalkToUnit);
    }

    private void Update() {

        switch (CurrentEnemyState) {
            case EnemyState.Idle:
                if (TargetBuilding == null) {
                    FindClosestBuilding();
                } else if (TargetUnit == null) {
                    FindClosestUnit();
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
                    _agent.SetDestination(TargetBuilding.transform.position);
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
                    SetState(EnemyState.WalkToBuilding);
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
                FindClosestBuilding();
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

            Collider[] allColliders = Physics.OverlapSphere(transform.position, DistanceToFollow * 2);
            
            //_enemyList = _enemyList.OrderBy(x => Vector3.Distance(point, x.transform.position)).ToList();
            float minDistance = Mathf.Infinity; // Расстояние до ближайшего здания - бесконечность
            Building closestBuilding = null; // Ближайшее здание не найдено

            for (int i = 0; i < allColliders.Length; i++) {
                Transform iParent = allColliders[i].gameObject.transform.parent;
                Building iBuilding = iParent.GetComponent<Building>();
                if (iBuilding) {
                    float distance = Vector3.Distance(transform.position, iBuilding.transform.position); // Расстояние до i-го здания
                    if (distance < minDistance) {
                        minDistance = distance;
                        closestBuilding = iBuilding;
                    }
                }
            }
            TargetBuilding = closestBuilding; // Ближайшее здание
            SetState(EnemyState.WalkToBuilding);
        }
    }

    private void FindClosestUnit() {
        if (TargetUnit == null) {
            Collider[] allColliders = Physics.OverlapSphere(transform.position, DistanceToFollow);
            
            float minDistance = Mathf.Infinity; // Расстояние до ближайшего юнита - бесконечность
            Unit closestUnit = null; // Ближайший юнит не найден
           
            for (int i = 0; i < allColliders.Length; i++) {
                Transform iParent = allColliders[i].gameObject.transform.parent;
                Unit iUnit = iParent.GetComponent<Unit>();
                if (iUnit) {
                    float distance = Vector3.Distance(transform.position, iUnit.transform.position); // Расстояние до i-го юнита
                    if (distance < minDistance) {
                        minDistance = distance;
                        closestUnit = iUnit;
                    }
                }
            }

            if (minDistance < DistanceToFollow) {
                TargetUnit = closestUnit; // Ближайший юнит
                SetState(EnemyState.WalkToUnit);
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
        Destroy(gameObject);
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
