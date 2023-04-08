using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitRotation : MonoBehaviour
{
    [Header("Obstacle Settings")]
    [Tooltip("Тип препятствия")]
    [SerializeField] private ObstacleType _type;
    [Tooltip("Статус препятствия")]
    [SerializeField] private bool _status = true;
    [Tooltip("Объект препятствия")]
    [SerializeField] private GameObject _object;
    [Tooltip("Минимальное отклонение")]
    [SerializeField] private float _minDeviation;
    [Tooltip("Максимальное отклонение")]
    [SerializeField] private float _maxDeviation;
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
