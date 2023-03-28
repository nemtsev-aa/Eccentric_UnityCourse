using UnityEngine;

public class RocketMove : MonoBehaviour
{
    [Tooltip("�������� �����������")]
    [SerializeField] private float _moveSpeed = 2f;
    [Tooltip("�������� ��������")]
    [SerializeField] private float _rotationSpeed = 3f;
    // ����
    private Transform _playerTransform;
    private bool _moveStatus;

    private void Start()
    {
        _playerTransform = FindObjectOfType<PlayerMove>().transform;
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
            transform.position += transform.forward * Time.deltaTime * _moveSpeed;
            // ���������� ����������� � ���� (������)
            Vector3 toPlayer = _playerTransform.position - transform.position;
            // ���������� �������
            Quaternion toPlayerRotation = Quaternion.LookRotation(toPlayer, Vector3.forward);
            // ������������ ������� ������ � ��������� ZX
            transform.rotation = Quaternion.Lerp(transform.rotation, toPlayerRotation, Time.deltaTime * _rotationSpeed);
        }
    }

}
