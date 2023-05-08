using UnityEngine;

public class HenMove : MonoBehaviour
{
    [Tooltip("���������� ���� ������")]
    [SerializeField] private Rigidbody _rigidbody;
    [Tooltip("C�������")]
    [SerializeField] private float _speed = 3f;

    // ��������� ����
    private Transform _targetTransform;

    public void Setup(Transform playerTransform)
    {
        _targetTransform = playerTransform;
    }

    private void Update()
    {
        // ������ �� �������� ��������� � ����
        Vector3 toTarget = (_targetTransform.position - transform.position);
        // ������� ���� ��������
        Quaternion targetRotation = Quaternion.LookRotation(toTarget);
        // ������� ����� � ����
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }

    private void FixedUpdate()
    {
        // ���� ��� �������������
        _rigidbody.velocity = transform.forward * _speed;
    }
}
