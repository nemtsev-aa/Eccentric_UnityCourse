using UnityEngine;

public class AcornMove : MonoBehaviour
{
    [Tooltip("���� ������")]
    [SerializeField] private Vector3 _velocity;
    [Tooltip("������������ ������� ��������")]
    [SerializeField] private float _maxRotationSpeed;

    private void Start()
    {
        // �������������� ���������� ��� ������ � ������ �����
        Rigidbody _acornRigidbody = GetComponent<Rigidbody>();
        // ������������ ��������� ���� �� ���� ������������ ��� ������������ ����� � �����
        _acornRigidbody.AddRelativeForce(_velocity, ForceMode.VelocityChange);
        _acornRigidbody.angularVelocity = new Vector3(
            Random.Range(-_maxRotationSpeed, _maxRotationSpeed),
            Random.Range(-_maxRotationSpeed, _maxRotationSpeed),
            Random.Range(-_maxRotationSpeed, _maxRotationSpeed));
    }
}
