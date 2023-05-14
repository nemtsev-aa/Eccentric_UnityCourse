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
    [Tooltip("Cтатус перемещения")]
    public MoveStatus CurrentMoveStatus;
    [Tooltip("Физическое тело игрока")]
    [SerializeField] private Rigidbody _rigidbody;
    [Tooltip("Скорость перемещения")]
    [SerializeField] private float _speed = 5f;
    [Tooltip("Джойстик")]
    [SerializeField] private Joystick _joystick;
    [Tooltip("Аниматор")]
    [SerializeField] private Animator _animator;
    
    private Vector2 _moveInput; // Положение, которое вернул джойстик

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
                transform.rotation = Quaternion.LookRotation(_rigidbody.velocity, Vector3.up); // Поворот персонажа в направлении приложения силы  
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
