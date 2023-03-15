using UnityEngine;

public class ObstacleRotation : MonoBehaviour
{
    [Header("Obstacle Settings")]
    [Tooltip("Статус препятствия")]
    [SerializeField] private bool _status;
    [Tooltip("Объект препятствия")]
    [SerializeField] private GameObject _object;
    [Tooltip("Тип препятствия")]
    [SerializeField] private ObstacleType _type;
    [Tooltip("Ось вращения")]
    [SerializeField] private Vector3 _rotationAxes;
    [Tooltip("Скорость вращения")]
    [SerializeField] private float _lerpSpeed;
    [Header("Materials")]
    [Tooltip("Материал препятствия")]
    [SerializeField] private Material _material;
    [Tooltip("Материал препятствия 1")]
    [SerializeField] private Material _material1;
    [Tooltip("Материал препятствия 2")]
    [SerializeField] private Material _material2;

    private void Start()
    {
        _type = ObstacleType.Rotation;
    }

    private void Update()
    {
        if (_status)
        {
            SetRotation();
            SetColor();
        }
    }

    private void SetRotation()
    {
       transform.Rotate(-_rotationAxes, Time.deltaTime * _lerpSpeed);
    }
    private void SetColor()
    {
        float interpolant = Mathf.Sin(Time.time * _lerpSpeed * 0.01f);
        _material.color = Color.Lerp(_material1.color, _material2.color, interpolant);
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
