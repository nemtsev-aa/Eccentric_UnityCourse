using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitRotation : MonoBehaviour
{
    [Header("Obstacle Settings")]
    [Tooltip("��� �����������")]
    [SerializeField] private ObstacleType _type;
    [Tooltip("������ �����������")]
    [SerializeField] private bool _status = true;
    [Tooltip("������ �����������")]
    [SerializeField] private GameObject _object;
    [Tooltip("����������� ����������")]
    [SerializeField] private float _minDeviation;
    [Tooltip("������������ ����������")]
    [SerializeField] private float _maxDeviation;
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

    private float _angleValue;

    private void Start()
    {
        _type = ObstacleType.LimitRotation;
        _angleValue = _minDeviation;

        SetLimitRotation(_minDeviation);
    }

    private void Update()
    {
        if (_status)
        {
            _angleValue = Mathf.MoveTowards(_angleValue, _maxDeviation, Time.deltaTime * _lerpSpeed);
            SetLimitRotation(_angleValue);
            SetColor(_angleValue);
        }

        if (_angleValue >= _maxDeviation)
        {
            _status = false;
        }
    }

    private void SetLimitRotation(float angleValue)
    {
        if (_rotationAxes.x > 0)
        {
            transform.eulerAngles = Vector3.right * angleValue;
        }
        else if (_rotationAxes.y > 0)
        {
            transform.eulerAngles = Vector3.up * angleValue;
        }
        else if (_rotationAxes.z > 0)
        {
            transform.eulerAngles = Vector3.forward * angleValue;
        } 
    }

    private void SetColor(float angleValue)
    {
        float interpolant = Mathf.InverseLerp(_minDeviation, _maxDeviation, _angleValue);
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
