using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Builder : Unit
{
    [Header("Builder Settings")]
    public LayerMask _layerMaskBuildings;
    public Building TargetBuilding;
    [SerializeField] private AudioClip _readSound;

    public override void Start() {
        base.Start();
        SetState(UnitState.Idle);
    }

    private void Update() {
        switch (CurrentUnitState) {
            case UnitState.Idle:
                if (TargetBuilding == null) {
                    //Debug.Log("FindBrokenBuilding");
                    FindBrokenBuilding();
                    if (TargetEnemy == null) {
                        //Debug.Log("FindClosestEnemy");
                        FindClosestEnemy(this);
                    }
                } else {
                    _animator.SetTrigger("Idle");
                    _animator.SetFloat("MoveSpeed", 0);
                }
                break;
            case UnitState.EscapeFromEnemy:
                if (!TargetBuilding) {
                    FindBrokenBuilding();
                }
                if (TargetEnemy) {
                    Vector3 fromEnemy = TargetEnemy.transform.position - transform.position;
                    
                    if (fromEnemy.sqrMagnitude < 5f) {
                        Vector2 randomPoint = Random.insideUnitCircle.normalized; 
                        Vector3 newPosition = new Vector3(randomPoint.x, 0f, randomPoint.y) * 7f + transform.position; 
                        TargetEnemy = null;
                        WhenClickOnGround(newPosition, _management);
                    } 
                }
                break;
            case UnitState.WalkToPoint:
                if (_targetPoint) {
                    if (_agent.velocity.magnitude > 0.5f) {
                        TargetPointPrefab.SetActive(true);
                        _animator.SetTrigger("Run");
                        _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
                    } else {
                        if (Vector3.Distance(transform.position, _targetPoint.transform.position) < 1f) {
                            Destroy(_targetPoint.gameObject);
                            _animator.SetTrigger("Idle");
                            _animator.SetFloat("MoveSpeed", 0);
                            CurrentUnitState = UnitState.Idle;
                        }
                    }
                }
                break;
            case UnitState.EnemyAttack:

                break;
            case UnitState.WalkToBrokenBuilding:
                if (TargetBuilding) {
                    _agent.SetDestination(TargetBuilding.transform.position);
                    float distanceToBuilding = Vector3.Distance(transform.position, TargetBuilding.transform.position);
                    if (distanceToBuilding > DistanceToFollow) SetState(UnitState.WalkToBrokenBuilding);
                    if (distanceToBuilding < DistanceToAttack) SetState(UnitState.RenovationBuilding);

                    ShowAnimation();
                } else {
                    SetState(UnitState.Idle);
                }
                break;
            case UnitState.RenovationBuilding:
                if (TargetBuilding) {
                    _agent.SetDestination(TargetBuilding.transform.position + new Vector3(1f, 0f, 1f));
                    float currentBuildingHealth = TargetBuilding.GetHealthProcentage();
                    if (currentBuildingHealth < 100) {
                        _animator.SetTrigger("Attack");
                        _animator.SetFloat("MoveSpeed", 0);
                        _timer += Time.deltaTime;
                        if (_timer > AttackPeriod) {
                            _timer = 0;
                            TargetBuilding.TakeHealth(DamagePerSecond*10);
                        }
                    } else {
                        _audioSource.clip = _readSound;
                        _audioSource.Play();
                        Vector2 randomPoint = Random.insideUnitCircle.normalized;
                        Vector3 newPosition = new Vector3(randomPoint.x, 0f, randomPoint.y) * 2f + transform.position;
                        TargetBuilding = null;
                        WhenClickOnGround(newPosition, _management);
                    }
                }
                break;
        }
    }

    private void FindBrokenBuilding() {
        if (TargetBuilding == null) {

            Collider[] allColliders = Physics.OverlapSphere(transform.position, DistanceToFollow * 2, _layerMaskBuildings);
            float minHealth = Mathf.Infinity; // Расстояние до ближайшего здания - бесконечность
            Building brokenBuilding = null; // Ближайшее здание не найдено

            for (int i = 0; i < allColliders.Length; i++) {
                GameObject iParent = allColliders[i].gameObject;
                Building iBuilding = iParent.GetComponent<Building>();                
                if (iBuilding) {
                    float currentBuildingHealth = iBuilding.GetHealthProcentage();

                    if (currentBuildingHealth < 100 && currentBuildingHealth < minHealth) {
                        minHealth = currentBuildingHealth;
                        brokenBuilding = iBuilding;
                    }
                }
            }

            if (brokenBuilding != null) {
                TargetBuilding = brokenBuilding; // Ближайшее здание
                SetState(UnitState.WalkToBrokenBuilding);
            } 
        }
    }

    private void ShowAnimation() {
        if (_agent.velocity.magnitude > 0) {
            _animator.SetTrigger("Run");
            _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
        } else {
            _animator.SetTrigger("Idle");
            _animator.SetFloat("MoveSpeed", 0);
        }

    }
}
