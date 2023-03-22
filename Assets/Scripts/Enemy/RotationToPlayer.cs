using UnityEngine;

public class RotationToPlayer : MonoBehaviour
{
    [Tooltip("Крайнее левое положение поворота")]
    [SerializeField] private Vector3 _leftEuler;
    [Tooltip("Крайнее правое положение поворота")]
    [SerializeField] private Vector3 _rightEuler;
    [Tooltip("Скорость поворота")]
    [SerializeField] private float _rotationSpeed = 5f;
    // Цель
    private Transform _rotationTarget;
    // Целевой угол поворота
    private Vector3 _targetEuler;

    private void Start()
    {
        _rotationTarget = FindObjectOfType<PlayerMove>().transform;
    }

    private void Update()
    {
        _targetEuler = (transform.position.x > _rotationTarget.position.x) ?  _rightEuler : _leftEuler;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_targetEuler), _rotationSpeed * Time.deltaTime);
    }
}
