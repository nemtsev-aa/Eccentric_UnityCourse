using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenMove : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    private Transform _targetTransform;

    [Tooltip("��������� ��������")]
    [SerializeField] private float _speed = 3f;
    [Tooltip("����� ��� ���������� ����")]
    [SerializeField] private float _timeToReachSpeed = 1f;

    private void Start()
    {
        _targetTransform = FindObjectOfType<PlayerMove>().transform;
    }

    private void FixedUpdate()
    {
        // ��������� ������ �� �������� ��������� �� ����
        Vector3 toTarget = (_targetTransform.position - transform.position).normalized;
        // ���� ��� �������������
        Vector3 force = _rigidbody.mass * (toTarget * _speed - _rigidbody.velocity) / _timeToReachSpeed;

        _rigidbody.AddForce(force,ForceMode.Impulse);
    }
}
