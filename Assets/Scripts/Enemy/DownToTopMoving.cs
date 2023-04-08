using UnityEngine;
using UnityEngine.Events;

public class DownToTopMoving : MonoBehaviour
{
    public Rigidbody Rigidbody;
    [Tooltip("Статус остановки")]
    public bool IsStopped;
    [Tooltip("Крайнее верхнее положение")]
    [SerializeField] private Transform _topPoint;
    [Tooltip("Крайнее нижнее положение")]
    [SerializeField] private Transform _downPoint;
    [Tooltip("Скорость движения")]
    [SerializeField] private float _moveSpeed;
    [Tooltip("Время остановки в конечной точке")]
    [SerializeField] private float _stopTime;
    [Tooltip("Текущее напрвление движения")]
    public Direction CurrentDirection;
    [Tooltip("Событие вызывающее движение вниз")]
    [SerializeField] private UnityEvent EventOnDownTarget;
    [Tooltip("Событие вызывающее движение вверх")]
    [SerializeField] private UnityEvent EventOnTopTarget;

    private Selectable _selectable;
    
    private void Start()
    {
        // Открепляем ограничивающие движение точки от модели
        _topPoint.parent = null;
        _downPoint.parent = null;
        transform.position = _downPoint.position;
        _selectable = GetComponent<Selectable>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (IsStopped)
        {
            _selectable.Show(true);
            return;
        }

        if (CurrentDirection == Direction.Down)
        {
            Rigidbody.MovePosition(transform.position - transform.up * _moveSpeed * Time.deltaTime);
            if (transform.position.y <= _downPoint.position.y)
            {
                CurrentDirection = Direction.Top;
                Debug.Log("Direction.Top");
                IsStopped = true;
                Invoke(nameof(ContinueMove), _stopTime);
                EventOnDownTarget.Invoke();
            }
        }
        else
        {
            Rigidbody.MovePosition(transform.position + transform.up * _moveSpeed * Time.deltaTime);
            if (transform.position.y >= _topPoint.position.y)
            {
                CurrentDirection = Direction.Down;
                Debug.Log("Direction.Down");
                IsStopped = true;
                Invoke(nameof(ContinueMove), _stopTime);
                EventOnTopTarget.Invoke();
            }
        }
    }

    public void ContinueMove()
    {
        IsStopped = false;
        _selectable.Show(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_topPoint.position, _downPoint.position);
    }
}