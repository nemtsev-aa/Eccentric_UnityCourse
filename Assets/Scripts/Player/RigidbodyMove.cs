using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MoveStatus
{
    Stop,
    Active
}

public class RigidbodyMove : MonoBehaviour
{
    [Tooltip("C����� �����������")]
    public MoveStatus CurrentMoveStatus;
    [Tooltip("���������� ���� ������")]
    [SerializeField] private Rigidbody _rigidbody;
    [Tooltip("�������� �����������")]
    [SerializeField] private float _speed = 5f;
    [Tooltip("��������")]
    [SerializeField] private Joystick _joystick;
    [Tooltip("��������")]
    [SerializeField] private Animator _animator;
    
    private Vector2 _moveInput; // ���������, ������� ������ ��������

    private void Start()
    {
        CurrentMoveStatus = MoveStatus.Stop;
    }

    private void FixedUpdate()
    {
        _moveInput = _joystick.Value;

        if (_joystick.IsPressed)
        {
            CurrentMoveStatus = MoveStatus.Active;
            _animator.SetBool("Run", true);
            _rigidbody.velocity = new Vector3(_moveInput.x, 0f, _moveInput.y) * _speed;
            if (_rigidbody.velocity != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(_rigidbody.velocity, Vector3.up); // ������� ��������� � ����������� ���������� ����  
        }
        else
        {
            CurrentMoveStatus = MoveStatus.Stop;
            _animator.SetBool("Run", false);
            _rigidbody.velocity = Vector3.zero;
        }
    }

    public void SpeedUpdate(float value)
    {
        _speed = value;
    }
}
