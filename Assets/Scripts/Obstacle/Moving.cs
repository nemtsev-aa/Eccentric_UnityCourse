using UnityEngine;

public class Moving : MonoBehaviour
{
    [Header("Obstacle Settings")]
    [Tooltip("��� �����������")]
    [SerializeField] private ObstacleType _type;
    [Tooltip("������ �����������")]
    [SerializeField] private bool _status = true;
    [Tooltip("������ �����������")]
    [SerializeField] private GameObject _object;
    [Tooltip("����������� ��������")]
    [SerializeField] private Vector3 _movingDirection;
    [Tooltip("�������� ��������")]
    [SerializeField] private float _lerpSpeed;
    [Header("Materials")]
    [Tooltip("����� �������� �������")]
    [SerializeField] private float _duration;
    [Tooltip("�������� �����������")]
    [SerializeField] private Material _material;
    [Tooltip("�������� ����������� 1")]
    [SerializeField] private Material _material1;
    [Tooltip("�������� ����������� 2")]
    [SerializeField] private Material _material2;

    private float _time = 0;
    void Update()
    {
        _time += Time.deltaTime;

        if (_status)
        {
            SetMovingValue();
            SetColor(_time);
        }

        if (_time >= _duration)
        {
            _time = 0;
        }      
    }
    private void SetMovingValue()
    {
        transform.position += _movingDirection * Time.deltaTime * _lerpSpeed;
    }
    private void SetColor(float time)
    {
        float interpolant = Mathf.InverseLerp(0, _duration, time);
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
