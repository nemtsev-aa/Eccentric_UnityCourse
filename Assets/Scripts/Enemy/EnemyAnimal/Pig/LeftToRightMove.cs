using UnityEngine;
using UnityEngine.Events;

public enum Direction
{
    Left,
    Right,
    Down,
    Top
}

public class LeftToRightMove : MonoBehaviour
{
    [Tooltip("������� ����� ���������")]
    [SerializeField] private Transform _topPoint;
    [Tooltip("������� ������ ���������")]
    [SerializeField] private Transform _downPoint;
    [Tooltip("�������� ��������")]
    [SerializeField] private float _moveSpeed;
    [Tooltip("����� ��������� � �������� �����")]
    [SerializeField] private float _stopTime;
    [Tooltip("������� ���������� ��������")]
    [SerializeField] private Direction _currentDirection;
    [Tooltip("�������� ���� PhysicRaycast")]
    [SerializeField] private Transform _rayStart;
    [Tooltip("������� ���������� ������� ������")]
    [SerializeField] private UnityEvent EventOnLeftTarget;
    [Tooltip("������� ���������� ������� �������")]
    [SerializeField] private UnityEvent EventOnRightTarget;


    //������ ���������
    private bool _isStopped;
   
    private void Start()
    {
        // ���������� �������������� �������� ����� �� ������
        _topPoint.parent = null;
        _downPoint.parent = null;
    }

    private void Update()
    {
        if (_isStopped)
        {
            return;
        }

        if (_currentDirection == Direction.Left)
        {
            transform.position -= new Vector3(Time.deltaTime * _moveSpeed, 0f, 0f);
            if (transform.position.x <= _topPoint.position.x)
            {
                _currentDirection = Direction.Right;
                _isStopped = true;
                Invoke(nameof(ContinueMove), _stopTime);
                EventOnLeftTarget.Invoke();
            }
        }
        else
        {
            transform.position += new Vector3(Time.deltaTime * _moveSpeed, 0f, 0f);
            if (transform.position.x >= _downPoint.position.x)
            {
                _currentDirection = Direction.Left;
                _isStopped = true;
                Invoke(nameof(ContinueMove), _stopTime);
                EventOnRightTarget.Invoke();
            }
        }

        if (_rayStart != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(_rayStart.position, Vector3.down, out hit))
            {
                transform.position = hit.point;
            }
        }
    }

    private void ContinueMove()
    {
        _isStopped = false;
    }
}
