using UnityEngine;
using UnityEngine.Events;

public class RotationToPlayer : MonoBehaviour
{
    [Tooltip("������� ����� ��������� ��������")]
    [SerializeField] private Vector3 _leftEuler;
    [Tooltip("������� ������ ��������� ��������")]
    [SerializeField] private Vector3 _rightEuler;
    [Tooltip("�������� ��������")]
    [SerializeField] private float _rotationSpeed = 5f;
    // ����
    private Transform _rotationTarget;
    // ������� ���� ��������
    private Vector3 _targetEuler;

    [SerializeField] private UnityEvent OnLeftRotation;
    [SerializeField] private UnityEvent OnRightRotation;

    private void Start()
    {
        _rotationTarget = FindObjectOfType<RigidbodyMove>().transform;
    }

    private void Update()
    {
        _targetEuler = (transform.position.x > _rotationTarget.position.x) ?  _rightEuler : _leftEuler;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_targetEuler), _rotationSpeed * Time.deltaTime);
    }

}
