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
        SetState(UnitState.Idle);
    }

    private void Update() {

        switch (CurrentUnitState) {
            case UnitState.Idle:
                FindClosestEnemy();
                break;
            case UnitState.WalkToPoint:
                FindClosestEnemy();
                if (_navMeshAgent.velocity.magnitude > 0) {
                    _animator.SetTrigger("Run");
                    _animator.SetFloat("MoveSpeed", _navMeshAgent.velocity.magnitude);
                }
                break;
            case UnitState.WalkToEnemy:
                if (TargetEnemy) {
                    _navMeshAgent.SetDestination(TargetEnemy.transform.position);
                    float distanceToEnemy = Vector3.Distance(transform.position, TargetEnemy.transform.position);
                    if (distanceToEnemy > DistanceToFollow) SetState(UnitState.WalkToPoint);
                    if (distanceToEnemy < DistanceToAttack) SetState(UnitState.EnemyAttack);

                    if (_navMeshAgent.velocity.magnitude > 0) {
                        _animator.SetTrigger("Run");
                        _animator.SetFloat("MoveSpeed", _navMeshAgent.velocity.magnitude);
                    }
                }
                else {
                    SetState(UnitState.WalkToPoint);
                }
                break;
            case UnitState.EnemyAttack:
                if (TargetEnemy) {
                    float distanceToTarget = Vector3.Distance(transform.position, TargetEnemy.transform.position);
                    if (distanceToTarget < DistanceToAttack) {
                        _animator.SetTrigger("Attack");
                        _timer += Time.deltaTime;
                        if (_timer > AttackPeriod) {
                            _timer = 0;
                            TargetEnemy.TakeDamage(DamagePerSecond);
                        }
                    }
                    else {
                        SetState(UnitState.WalkToEnemy);
                    }
                }
                else {
                    SetState(UnitState.WalkToEnemy);
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
                if (TargetPoint) {
                    _navMeshAgent.SetDestination(TargetPoint.transform.position);
                }
                break;
            case UnitState.WalkToEnemy:
                FindClosestEnemy();
                if (TargetEnemy) {
                    _navMeshAgent.SetDestination(TargetEnemy.transform.position);
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
            Collider[] allColliders = Physics.OverlapSphere(transform.position, DistanceToFollow);

            float minDistance = Mathf.Infinity; // Расстояние до ближайшего юнита - бесконечность
            Enemy closestEnemy = null; // Ближайший юнит не найден

            for (int i = 0; i < allColliders.Length; i++) {
                Transform iParent = allColliders[i].gameObject.transform.parent;
                Enemy iEnemy = iParent.GetComponent<Enemy>();
                if (iEnemy) {
                    float distance = Vector3.Distance(transform.position, iEnemy.transform.position); // Расстояние до i-го юнита
                    if (distance < minDistance) {
                        minDistance = distance;
                        closestEnemy = iEnemy;
                    }
                }
            }

            if (minDistance < DistanceToFollow) {
                TargetEnemy = closestEnemy; // Ближайший враг
                Debug.Log("TargetEnemy: " + TargetEnemy.gameObject.name);
                SetState(UnitState.WalkToEnemy);
            }
        }
    }

    //private void FindClosestEnemy() {
    //    Enemy[] allEnemies = FindObjectsOfType<Enemy>(); // Массив всех врагов на карте
    //    float minDistance = Mathf.Infinity; // Расстояние до ближайшего врага - бесконечность
    //    Enemy closestEnemy = null; // Ближайший враг не найден
    //    for (int i = 0; i < allEnemies.Length; i++) {
    //        float distance = Vector3.Distance(transform.position, allEnemies[i].transform.position); // Расстояние до i-го врага
    //        if (distance < minDistance) {
    //            minDistance = distance;
    //            closestEnemy = allEnemies[i];
    //        }
    //    }

    //    if (minDistance < DistanceToFollow) {
    //        TargetEnemy = closestEnemy; // Ближайшее здание
    //        SetState(UnitState.WalkToEnemy);
    //    }
    //}

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, DistanceToAttack);
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.up, DistanceToFollow);
    }
#endif

}
