using UnityEngine;

public class HenMove : MonoBehaviour
{
    [Tooltip("Физическое тело курицы")]
    [SerializeField] private Rigidbody _rigidbody;
    [Tooltip("Cкорость")]
    [SerializeField] private float _speed = 3f;
    [Tooltip("Cкорость")]
    [SerializeField] private float _rotationLerpRate = 3f;

    // Трансформ цели
    private Transform _targetTransform;

    public void Setup(Transform playerTransform)
    {
        _targetTransform = playerTransform;
    }

    private void Update()
    {
        // Вектор от текущего положения к цели
        Vector3 toTarget = _targetTransform.position - transform.position;
        // Целевой угол поворота
        Quaternion targetRotation = Quaternion.LookRotation(toTarget, Vector3.up);
        // Поворот врага к цели
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationLerpRate);
    }

    private void FixedUpdate()
    {
        // Сила для преследования
        _rigidbody.velocity = transform.forward * _speed;
    }
}
