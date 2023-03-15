using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotMove : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    private Transform _targetTransform;

    [Tooltip("C�������")]
    [SerializeField] private float _speed = 3f;

    private void Start()
    {
        _targetTransform = FindObjectOfType<PlayerMove>().transform;
        // ��������� ������ �� �������� ��������� �� ����
        Vector3 toTarget = (_targetTransform.position - transform.position).normalized;
        _rigidbody.velocity = toTarget * _speed;
    }
}
