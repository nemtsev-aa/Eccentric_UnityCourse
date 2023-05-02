using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyMove : MonoBehaviour
{
    [Tooltip("��� �����")]
    [SerializeField] private Rigidbody _rigidbody;
    [Tooltip("�������� �����������")]
    [SerializeField] private float _speed = 5f;
    [Tooltip("��������")]
    [SerializeField] private Joystick _joystick;
    [Tooltip("��������")]
    [SerializeField] private Animator _animator;
    private Vector2 _moveInput; // ���������, ������� ����� ��������

    void Update()
    {
        _moveInput = _joystick.Value;
        _animator.SetBool("Run", _joystick.IsPressed); 
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_moveInput.x, 0f, _moveInput.y) * _speed;

        if (_rigidbody.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity, Vector3.up); // ������� ��������� � ����������� ���������� ����
        }
    }
}
