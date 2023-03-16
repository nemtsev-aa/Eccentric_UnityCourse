using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotMove : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    private Transform _targetTransform;
    private Transform _creatorTransform;
    private bool _moveStatus;

    [Tooltip("Cкорость")]
    [SerializeField] private float _speed = 3f;

    private void Update()
    {
        if (_moveStatus)
        {
            MoveToTarget();
        }
        else
        {
            MoveToCreator();
        }
        
    }

    public void SetCarrotCreator(Transform carrorCreatorTransform)
    {
        _creatorTransform = carrorCreatorTransform;
    }

    public void MoveToTarget()
    {
        _moveStatus = true;
        _targetTransform = FindObjectOfType<PlayerMove>().transform;
        // Единичный вектор от текущего положения до цели
        Vector3 toTarget = (_targetTransform.position - transform.position).normalized;
        _rigidbody.velocity = toTarget * _speed;
    }

    public void MoveToCreator()
    {
        transform.position = _creatorTransform.position;
        _moveStatus = false;
    }
}
