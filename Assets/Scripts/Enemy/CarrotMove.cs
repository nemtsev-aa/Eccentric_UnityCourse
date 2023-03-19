using UnityEngine;

public class CarrotMove : MonoBehaviour
{
    [Tooltip("���������� ���� �������")] 
    [SerializeField] private Rigidbody _rigidbody;
    [Tooltip("C�������")]
    [SerializeField] private float _speed = 3f;
    // ��������� ����
    private Transform _targetTransform;
    // ������� ����� ���������
    private Transform _creatorTransform;
    // ������ �����������
    private bool _moveStatus;

    private void Update()
    {
        if (_moveStatus)
        {
            // ������� �������� � ����
            MoveToTarget();
        }
        else
        {
            // ������� ������� �� ������ ���������
            MoveToCreator();
        } 
    }

    public void SetCarrotCreator(Transform carrorCreatorTransform)
    {
        _creatorTransform = carrorCreatorTransform;
    }

    public void MoveToTarget()
    {
        _moveStatus = true;
        // ���� ������� - ��������
        _targetTransform = FindObjectOfType<PlayerMove>().transform;
        
        if (_targetTransform != null)
        {
            // ��������� ������ �� �������� ��������� ������� �� ����
            Vector3 toTarget = (_targetTransform.position - transform.position).normalized;
            _rigidbody.velocity = toTarget * _speed;
        }
    }

    public void MoveToCreator()
    {
        if (_creatorTransform != null)
        {
            transform.position = _creatorTransform.position;
            _moveStatus = false;
        }  
        else
        {
            Destroy(gameObject);
        }
    }
}
