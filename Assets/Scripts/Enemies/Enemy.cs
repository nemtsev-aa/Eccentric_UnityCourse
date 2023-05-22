using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState {
    Idle,
    Attack,
    WalkToBuilding,
    WalkToUnit
}
public class Enemy : MonoBehaviour
{
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

    [Header("Targets")]
    [Tooltip("���� - ������")]
    public Building TargetBuilding;
    [Tooltip("���� - ����")]
    public Unit TargetUnit;


    [Tooltip("��������")]
    [SerializeField] private Animator _animator;
    [Tooltip("�� �����������")]
    [SerializeField] private NavMeshAgent _agent;

    private float _timer;

    private void Start() {
        SetState(EnemyState.WalkToBuilding);
    }

    private void Update() {
        
        switch (CurrentEnemyState) {
            case EnemyState.Idle:
                FindClosestUnit();
                break;
            case EnemyState.WalkToBuilding:
                FindClosestUnit();
                if (TargetBuilding == null) {
                    SetState(EnemyState.Idle);
                } else {
                    float distanceToBuilding = Vector3.Distance(transform.position, TargetBuilding.transform.position);
                    if (distanceToBuilding > DistanceToAttack) SetState(EnemyState.WalkToBuilding);
                    if (distanceToBuilding < DistanceToAttack) SetState(EnemyState.Attack);
                }
                
                if (_agent.velocity.magnitude > 0) {
                    _animator.SetTrigger("Run");
                    _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
                }
                break;
            case EnemyState.WalkToUnit:
                if (TargetUnit) {
                    _agent.SetDestination(TargetUnit.transform.position);
                    float distanceToUnit = Vector3.Distance(transform.position, TargetUnit.transform.position);
                    if (distanceToUnit > DistanceToFollow) SetState(EnemyState.WalkToBuilding);
                    if (distanceToUnit < DistanceToAttack) SetState(EnemyState.Attack);

                    if (_agent.velocity.magnitude > 0) {
                        _animator.SetTrigger("Run");
                        _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
                    }
                } else {
                    SetState(EnemyState.WalkToBuilding);
                }
                break;
            case EnemyState.Attack:
                if (TargetUnit) {
                    float distanceToTarget = Vector3.Distance(transform.position, TargetUnit.transform.position);
                    if (distanceToTarget <= DistanceToAttack) {
                        TakeDamage();
                    } else {
                        SetState(EnemyState.WalkToUnit);
                    }
                } else if (TargetBuilding) {
                    float distanceToBuilding = Vector3.Distance(transform.position, TargetBuilding.transform.position);
                    if (distanceToBuilding <= DistanceToAttack) {
                        TakeDamage();
                    } else {
                        SetState(EnemyState.WalkToBuilding);
                    }
                }
                else {
                    SetState(EnemyState.WalkToBuilding);
                }
                break;
            default:
                break;
        }
    }

    private void TakeDamage() {
        _animator.SetTrigger("Attack");
        _timer += Time.deltaTime;
        if (_timer > AttackPeriod) {
            _timer = 0;
            TargetBuilding.TakeDamage(DamagePerSecond);
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
                FindClosestUnit();
                if (TargetUnit) {
                    _agent.SetDestination(TargetUnit.transform.position);
                }
                break;
            case EnemyState.Attack:
                _timer = 0f;
                _animator.SetTrigger("Attack");
                break;
            default:
                break;
        }
    }

    private void FindClosestBuilding() {
        Building[] allBuildings = FindObjectsOfType<Building>(); // ������ ���� ������ �� �����
        float minDistance = Mathf.Infinity; // ���������� �� ���������� ������ - �������������
        Building closestBuilding = null; // ��������� ������ �� �������
        for (int i = 0; i < allBuildings.Length; i++) {
            float distance = Vector3.Distance(transform.position, allBuildings[i].transform.position); // ���������� �� i-�� ������
            if (distance < minDistance) {
                minDistance = distance;
                closestBuilding = allBuildings[i]; 
            }
        }
        TargetBuilding = closestBuilding; // ��������� ������
    }

    private void FindClosestUnit() {
        Unit[] allUnits = FindObjectsOfType<Unit>(); // ������ ���� ������ �� �����
        float minDistance = Mathf.Infinity; // ���������� �� ���������� ����� - �������������
        Unit closestUnit = null; // ��������� ���� �� ������
        for (int i = 0; i < allUnits.Length; i++) {
            float distance = Vector3.Distance(transform.position, allUnits[i].transform.position); // ���������� �� i-�� �����
            if (distance < minDistance) {
                minDistance = distance;
                closestUnit = allUnits[i];
            }
        }

        if (minDistance < DistanceToFollow) {
            TargetUnit = closestUnit; // ��������� ������
            SetState(EnemyState.WalkToUnit);
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
