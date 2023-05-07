using UnityEngine;

public class RocketMove : MonoBehaviour
{
    [Tooltip("�������� �����������")]
    public float MoveSpeed = 2f;
    [Tooltip("�������� ��������")]
    public float RotationSpeed = 3f;
    // ����
    private Transform _playerTransform;
    private bool _moveStatus;

    private void Start()
    {
        _playerTransform = FindObjectOfType<RigidbodyMove>().transform;
    }

    public void StartMove()
    {
        _moveStatus = true;
    }

    private void Update()
    {
        if (_moveStatus && _playerTransform != null)
        {
            transform.localPosition = new Vector3(transform.position.x, transform.position.y, 0f);
            // ���������� ������ � ����������� ��������� ��� Z
            transform.position += transform.forward * Time.deltaTime * MoveSpeed;
            // ���������� ����������� � ���� (������)
            Vector3 toPlayer = _playerTransform.position - transform.position;
            // ���������� �������
            Quaternion toPlayerRotation = Quaternion.LookRotation(toPlayer, Vector3.forward);
            // ������������ ������� ������ � ��������� ZX
            transform.rotation = Quaternion.Lerp(transform.rotation, toPlayerRotation, Time.deltaTime * RotationSpeed);
        }
    }

}
