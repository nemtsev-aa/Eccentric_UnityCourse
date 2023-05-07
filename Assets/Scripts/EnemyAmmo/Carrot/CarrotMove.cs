using UnityEngine;

public class CarrotMove : MonoBehaviour
{
    [Tooltip("Физическое тело моркови")] 
    [SerializeField] private Rigidbody _rigidbody;
    [Tooltip("Cкорость")]
    [SerializeField] private float _speed = 3f;
    // Трансформ цели
    private Transform _targetTransform;
    // Позиция точки появления
    private Transform _creatorTransform;
    // Статус перемещения
    private bool _moveStatus;

    private void Update()
    {
        if (_moveStatus)
        {
            // Морковь движется к цели
            MoveToTarget();
        }
        else
        {
            // Морковь следует за точкой появления
            MoveToCreator();
        } 
    }

    public void SetCarrotCreator(Transform carrorCreatorTransform)
    {
        _creatorTransform = carrorCreatorTransform;
    }

    public void MoveToTarget()
    {
        // Цель моркови - персонаж
        _targetTransform = FindObjectOfType<RigidbodyMove>().transform;
        
        if (_targetTransform != null)
        {
            // Перемещаем объект в плоскость игры Z = 0
            transform.localPosition = new Vector3(transform.position.x, transform.position.y, 0f);
            // Поворачиваем объект
            transform.rotation = Quaternion.identity;
            // Единичный вектор от текущего положения моркови до цели
            Vector3 toTarget = (_targetTransform.position - transform.position).normalized;
            _rigidbody.velocity = toTarget * _speed;
        }
    }

    public void StartMove()
    {
        _moveStatus = true;
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
