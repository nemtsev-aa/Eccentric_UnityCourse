using System.Collections;
using System.Collections.Generic;
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
                break;
            case EnemyState.WalkToUnit:
                _agent.SetDestination(TargetUnit.transform.position);
                float distanceToUnit = Vector3.Distance(TargetUnit.transform.position, transform.position);
                if (distanceToUnit > DistanceToFollow) SetState(EnemyState.WalkToBuilding);
                if (distanceToUnit < DistanceToAttack) SetState(EnemyState.Attack);
                break;
            case EnemyState.Attack:
                if (TargetUnit) {
                    float distanceToTarget = Vector3.Distance(TargetUnit.transform.position, transform.position);
                    if (distanceToTarget > DistanceToAttack) {
                        SetState(EnemyState.WalkToUnit);
                    }
                    _timer += Time.deltaTime;
                    if (_timer > 1f) {
                        TargetUnit.TakeDamage(DamagePerSecond);
                    }
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
                    _animator.SetTrigger("Run");
                    _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
                }
                break;
            case EnemyState.WalkToUnit:

                _animator.SetTrigger("Run");
                _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
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
}
