using UnityEngine;

public class LimitMoving : MonoBehaviour
{
    [Header("Obstacle Settings")]
    [Tooltip("Тип препятствия")]
    [SerializeField] private ObstacleType _type;
    [Tooltip("Статус препятствия")]
    [SerializeField] private bool _status = true;
    [Tooltip("Объект препятствия")]
    [SerializeField] private GameObject _object;
    [Tooltip("Время действия эффекта")]
    [SerializeField] private float _duration;
    [Tooltip("Нижнее положение")]
    [SerializeField] private Transform _minPosition;
    [Tooltip("Верхнее положение")]
    [SerializeField] private Transform _maxPosition;
        
    [Header("Materials")]
    [Tooltip("Материал препятствия")]
    [SerializeField] private Material _material;
    [Tooltip("Материал препятствия 1")]
    [SerializeField] private Material _material1;
    [Tooltip("Материал препятствия 2")]
    [SerializeField] private Material _material2;

    [Range (0,1)]
    public float _movingValue;
    private float _time = 0;

    private void Start()
    {
        _type = ObstacleType.LimitMoving;
        SetLimitMoving(0);
    }

    private void Update()
    {
        SetLimitMoving(_movingValue);

        if (_status)
        {
            _time += Time.deltaTime;

            if (_time >= _duration)
            {
                _time = 0;
                _status = !_status;
            }
            else
            {
                _movingValue = Mathf.InverseLerp(0, _duration, _time);
                SetLimitMoving(_movingValue);
                SetColor(_movingValue);
            }
        }
    }

    private void SetLimitMoving(float movingValue)
    {
        _object.transform.position = Vector3.Lerp(_minPosition.position, _maxPosition.position, movingValue);
        _object.transform.rotation = Quaternion.Lerp(_minPosition.rotation, _maxPosition.rotation, movingValue);
    }

    private void SetColor(float movingValue)
    {
        _material.color = Color.Lerp(_material1.color, _material2.color, movingValue);
    }

    public bool GetStatus()
    {
        return _status;
    }

    public void SetStatus(bool status)
    {
        _status = status;
    }

    public ObstacleType GetObstacleType()
    {
        return _type;
    }

}
