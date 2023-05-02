using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyMove : MonoBehaviour
{
    [Tooltip("Тип ввода")]
    [SerializeField] private Rigidbody _rigidbody;
    [Tooltip("Скорость перемещения")]
    [SerializeField] private float _speed = 5f;
    [Tooltip("Джойстик")]
    [SerializeField] private Joystick _joystick;
    [Tooltip("Аниматор")]
    [SerializeField] private Animator _animator;
    private Vector2 _moveInput; // Положение, которое венул джойстик

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
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity, Vector3.up); // Поворот персонажа в направлении приложения силы
        }
    }
}
