using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotMove : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    private Transform _targetTransform;

    [Tooltip("Cкорость")]
    [SerializeField] private float _speed = 3f;

    private void Start()
    {
        _targetTransform = FindObjectOfType<PlayerMove>().transform;
        // Единичный вектор от текущего положения до цели
        Vector3 toTarget = (_targetTransform.position - transform.position).normalized;
        _rigidbody.velocity = toTarget * _speed;
    }
}
