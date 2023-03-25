using UnityEngine;
using UnityEngine.Events;

public enum Direction
{
    Left,
    Right
}

public class LeftToRightMove : MonoBehaviour
{
    [Tooltip("Крайнее левое положение")]
    [SerializeField] private Transform _leftPoint;
    [Tooltip("Крайнее правое положение")]
    [SerializeField] private Transform _rightPoint;
    [Tooltip("Скорость движения")]
    [SerializeField] private float _moveSpeed;
    [Tooltip("Время остановки в конечной точке")]
    [SerializeField] private float _stopTime;
    [Tooltip("Текущее напрвление движения")]
    [SerializeField] private Direction _currentDirection;
    [Tooltip("Источник луча PhysicRaycast")]
    [SerializeField] private Transform _rayStart;
    [Tooltip("Событие вызывающее поворот налево")]
    [SerializeField] private UnityEvent EventOnLeftTarget;
    [Tooltip("Событие вызывающее поворот направо")]
    [SerializeField] private UnityEvent EventOnRightTarget;


    //Статус остановки
    private bool _isStopped;
   
    private void Start()
    {
        // Открепляем ограничивающие движение точки от модели
        _leftPoint.parent = null;
        _rightPoint.parent = null;
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
            if (transform.position.x <= _leftPoint.position.x)
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
            if (transform.position.x >= _rightPoint.position.x)
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
