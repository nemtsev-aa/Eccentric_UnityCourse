using UnityEngine;

public class HenMove : MonoBehaviour
{
    [Tooltip("Физическое тело курицы")]
    [SerializeField] private Rigidbody _rigidbody;
    [Tooltip("Начальная скорость")]
    [SerializeField] private float _speed = 3f;
    [Tooltip("Время для достижения цели")]
    [SerializeField] private float _timeToReachSpeed = 1f;
    // Трансформ цели
    private Transform _targetTransform;

    private void Start()
    {
        _targetTransform = FindObjectOfType<RigidbodyMove>().transform;
    }

    private void FixedUpdate()
    {
        // Единичный вектор от текущего положения до цели
        Vector3 toTarget = (_targetTransform.position - transform.position);
        // Сила для преследования
        Vector3 force = _rigidbody.mass * (toTarget * _speed - _rigidbody.velocity) / _timeToReachSpeed;

        _rigidbody.AddForce(force, ForceMode.Impulse);
        _rigidbody.rotation = Quaternion.LookRotation(-_rigidbody.velocity, Vector3.up);
    }
}
