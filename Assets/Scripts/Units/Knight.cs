using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public enum KnightType {
    Standart,
    Light,
    Heavy
}
public class Knight : Unit
{
    [Space(10)]
    public KnightType KnightType;
    
   

    public override void Start() {
        base.Start();
        SetState(UnitState.Idle);
    }

    private void Update() {

        switch (CurrentUnitState) {
            case UnitState.Idle:
                _animator.SetTrigger("Idle");
                if (TargetEnemy == null) {
                    FindClosestEnemy(this);
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
                FindClosestEnemy(this);
                
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

    public override void SetState(UnitState UnitState) {
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
                FindClosestEnemy(this);
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
}
