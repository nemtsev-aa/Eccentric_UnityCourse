using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public enum UnitState {
    Idle,
    EnemyAttack,
    WalkToPoint,
    WalkToEnemy,
    EscapeFromEnemy,
    WalkToBrokenBuilding,
    RenovationBuilding
}

public enum UnitType {
    Knight,
    Farmer,
    Builder,
    Scout
}
public class Unit : SelectableObject {
    [Tooltip("��� �����")]
    public UnitType UnitType;
    [Tooltip("������� ���������")]
    public UnitState CurrentUnitState;

    [Header("Parametrs")]
    [Tooltip("����")]
    public int Price;
    [Tooltip("���������� ��������")]
    public int Health;
    [Tooltip("���")]
    public int DamagePerSecond;
    [Tooltip("������ �����")]
    public float AttackPeriod = 0.1f;
    [Tooltip("������ ���������")]
    public float DistanceToFollow = 7f;
    [Tooltip("������ �����")]
    public float DistanceToAttack = 1f;
    [Tooltip("������� �����")]
    public GameObject HealthBarPrefab;
    [Tooltip("�������������")]
    public GameObject TargetPointPrefab;
    public LayerMask _layerMask;

    [Space(10)]
    [Header("FX Effects")]
    [SerializeField] private AudioClip _selectionSound;
    [SerializeField] private ParticleSystem _damageEffect;
    [Space(10)]
    [SerializeField] protected Animator _animator;
    [SerializeField] protected NavMeshAgent _agent;

    [Header("Targets")]

    [Tooltip("���� - ����")]
    public Enemy TargetEnemy;

    private int _maxHealth;
    protected AudioSource _audioSource;
    private HealthBar _healthBar;
    protected GameObject _targetPoint;
    protected Management _management;
    protected float _timer;

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

    public override void WhenClickOnGround(Vector3 point, Management management) {
        base.WhenClickOnGround(point, management);
        _management = management;
        if (_targetPoint != null) {
            Destroy(_targetPoint.gameObject);
        }
        _targetPoint = Instantiate(TargetPointPrefab, point, Quaternion.identity);
        CurrentUnitState = UnitState.WalkToPoint;
        _agent.SetDestination(point);
    }

    public void TakeDamage(int damageValue) {
        Health -= damageValue;
        _healthBar.SetHealth(Health, _maxHealth);
        ParticleSystem damageEffect = Instantiate(_damageEffect, transform.position, Quaternion.identity);
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
        if (_management) {
            _management.Unselect(this);
        }
        if (_healthBar) {
            Destroy(_healthBar.gameObject);
        }
    }

    protected void FindClosestEnemy(Unit unit) {
        if (TargetEnemy == null) {
            Collider[] allColliders = Physics.OverlapSphere(transform.position, DistanceToFollow, _layerMask);
            //Debug.Log("FindClosestEnemy: " + allColliders.Length);
            
            float minDistance = Mathf.Infinity; // ���������� �� ���������� ����� - �������������
            Enemy closestEnemy = null; // ��������� ���� �� ������

            for (int i = 0; i < allColliders.Length; i++) {
                Transform iParent = allColliders[i].gameObject.transform.parent;
                Enemy iEnemy = iParent.GetComponent<Enemy>();
                if (iEnemy) {
                    float distance = Vector3.Distance(transform.position, iEnemy.transform.position); // ���������� �� i-�� �����
                    if (distance < minDistance) {
                        minDistance = distance;
                        closestEnemy = iEnemy;
                    }
                }
            }

            if (minDistance < DistanceToFollow && closestEnemy != null) {
                TargetEnemy = closestEnemy; // ��������� ����
                Debug.Log("TargetEnemy: " + TargetEnemy.gameObject.name);
                switch (unit.UnitType) {
                    case UnitType.Knight:
                        SetState(UnitState.WalkToEnemy);
                        break;
                    case UnitType.Farmer:
                        SetState(UnitState.EscapeFromEnemy);
                        break;
                    case UnitType.Builder:
                        SetState(UnitState.EscapeFromEnemy);
                        break;
                } 
            }
        }
    }

    public virtual void SetState(UnitState UnitState) {
        CurrentUnitState = UnitState;
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
