using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
public enum UnitState {
    Idle,
    EnemyAttack,
    WalkToPoint,
    WalkToEnemy
}
public enum KnightType {
    Standart,
    Light,
    Heavy
}
public class Knight : Unit
{
    [Space(10)]
    public KnightType KnightType;
    
    private float _timer;

    public override void Start() {
        base.Start();
        SetState(UnitState.WalkToEnemy);
    }

    private void Update() {

        switch (CurrentUnitState) {
            case UnitState.Idle:
                _animator.SetTrigger("Idle");
                if (TargetEnemy == null) {
                    FindClosestEnemy();
                }
                break;
            case UnitState.WalkToPoint:
                if (_targetPoint) {
                    if (_agent.velocity.magnitude > 0) {
                        TargetPointPrefab.SetActive(true);
                        _animator.SetTrigger("Run");
                        _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
                    } else {
                        if (Vector3.Distance(transform.position, _targetPoint.transform.position) < 1f) {
                            Destroy(_targetPoint.gameObject);
                            CurrentUnitState = UnitState.Idle;
                        }
                    }
                }
                FindClosestEnemy();
                
                break;
            case UnitState.WalkToEnemy:
                if (TargetEnemy) {
                    _agent.SetDestination(TargetEnemy.transform.position);
                    float distanceToEnemy = Vector3.Distance(transform.position, TargetEnemy.transform.position);
                    if (distanceToEnemy > DistanceToFollow) SetState(UnitState.WalkToPoint);
                    if (distanceToEnemy < DistanceToAttack) SetState(UnitState.EnemyAttack);

                    if (_agent.velocity.magnitude > 0) {
                        _animator.SetTrigger("Run");
                        _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
                    }
                } else {
                    SetState(UnitState.Idle);
                }
                break;
            case UnitState.EnemyAttack:
                if (TargetEnemy) {
                    _agent.SetDestination(TargetEnemy.transform.position);
                    float distanceToTarget = Vector3.Distance(transform.position, TargetEnemy.transform.position);
                    if (distanceToTarget < DistanceToAttack) {
                        _animator.SetTrigger("Attack");
                        _timer += Time.deltaTime;
                        if (_timer > AttackPeriod) {
                            _timer = 0;
                            TargetEnemy.TakeDamage(DamagePerSecond);
                        }
                    } else {
                        SetState(UnitState.WalkToEnemy);
                    }
                } else {
                    SetState(UnitState.Idle);
                }
                break;
            default:
                break;
        }
    }

    public void SetState(UnitState UnitState) {
        CurrentUnitState = UnitState;
        switch (CurrentUnitState) {
            case UnitState.Idle:
                _animator.SetTrigger("Idle");
                break;
            case UnitState.WalkToPoint:
                if (TargetPointPrefab != null) {
                    _agent.SetDestination(TargetPointPrefab.transform.position);
                }
                break;
            case UnitState.WalkToEnemy:
                FindClosestEnemy();
                if (TargetEnemy) {
                    _agent.SetDestination(TargetEnemy.transform.position);
                } else {
                    SetState(UnitState.Idle);
                }
                break;
            case UnitState.EnemyAttack:
                _timer = 0f;
                break;
            default:
                break;
        }
    }
    private void FindClosestEnemy() {
        if (TargetEnemy == null) {
            Collider[] allColliders = Physics.OverlapSphere(transform.position, DistanceToFollow, LayerMask.NameToLayer("Enemy"));
            Debug.Log(allColliders.Length);
            float minDistance = Mathf.Infinity; // Расстояние до ближайшего юнита - бесконечность
            Enemy closestEnemy = null; // Ближайший юнит не найден

            for (int i = 0; i < allColliders.Length; i++) {
                Transform iParent = allColliders[i].gameObject.transform;
                Enemy iEnemy = iParent.GetComponent<Enemy>();
                if (iEnemy) {
                    float distance = Vector3.Distance(transform.position, iEnemy.transform.position); // Расстояние до i-го юнита
                    if (distance < minDistance) {
                        minDistance = distance;
                        closestEnemy = iEnemy;
                    }
                }
            }

            if (minDistance < DistanceToFollow && closestEnemy != null) {
                TargetEnemy = closestEnemy; // Ближайший враг
                Debug.Log("TargetEnemy: " + TargetEnemy.gameObject.name);
                SetState(UnitState.WalkToEnemy);
            }
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
