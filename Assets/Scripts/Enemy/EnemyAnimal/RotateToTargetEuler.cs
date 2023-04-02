using UnityEngine;

public class RotateToTargetEuler : MonoBehaviour
{
    [Tooltip("Максимальный угол поворота налево")]
    [SerializeField] private Vector3 _leftEuler;
    [Tooltip("Максимальный угол поворота направо")]
    [SerializeField] private Vector3 _rightEuler;
    [Tooltip("Скорость разворота")]
    [SerializeField] private float _rotationSpeed;
    // Целевой угол поворота
    private Vector3 _targetEuler;

    private void Update()
    {
        // Правно поворачиваем объект к цели
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(_targetEuler), Time.deltaTime * _rotationSpeed);
    }

    public void RotateLeft()
    {
        // Переключаем целевой угол поворота - налево
        _targetEuler = _leftEuler;
    }
    public void RotateRight()
    {
        // Переключаем целевой угол поворота - направо
        _targetEuler = _rightEuler;
    }
}
