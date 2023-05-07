using UnityEngine;

public class HenMove : MonoBehaviour
{
    [Tooltip("���������� ���� ������")]
    [SerializeField] private Rigidbody _rigidbody;
    [Tooltip("��������� ��������")]
    [SerializeField] private float _speed = 3f;
    [Tooltip("����� ��� ���������� ����")]
    [SerializeField] private float _timeToReachSpeed = 1f;
    // ��������� ����
    private Transform _targetTransform;

    private void Start()
    {
        _targetTransform = FindObjectOfType<RigidbodyMove>().transform;
    }

    private void FixedUpdate()
    {
        // ��������� ������ �� �������� ��������� �� ����
        Vector3 toTarget = (_targetTransform.position - transform.position);
        // ���� ��� �������������
        Vector3 force = _rigidbody.mass * (toTarget * _speed - _rigidbody.velocity) / _timeToReachSpeed;

        _rigidbody.AddForce(force, ForceMode.Impulse);
        _rigidbody.rotation = Quaternion.LookRotation(-_rigidbody.velocity, Vector3.up);
    }
}
