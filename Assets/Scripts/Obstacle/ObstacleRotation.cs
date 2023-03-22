using UnityEngine;

public class ObstacleRotation : MonoBehaviour
{
    [Header("Obstacle Settings")]
    [Tooltip("������ �����������")]
    [SerializeField] private bool _status;
    [Tooltip("������ �����������")]
    [SerializeField] private GameObject _object;
    [Tooltip("��� �����������")]
    [SerializeField] private ObstacleType _type;
    [Tooltip("��� ��������")]
    [SerializeField] private Vector3 _rotationAxes;
    [Tooltip("�������� ��������")]
    [SerializeField] private float _lerpSpeed;
    [Header("Materials")]
    [Tooltip("�������� �����������")]
    [SerializeField] private Material _material;
    [Tooltip("�������� ����������� 1")]
    [SerializeField] private Material _material1;
    [Tooltip("�������� ����������� 2")]
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
