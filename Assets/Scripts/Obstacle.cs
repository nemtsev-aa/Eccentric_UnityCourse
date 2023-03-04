using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType
{
    Disappear,
    Rotation,
    Moving
}

public class Obstacle : MonoBehaviour
{
    [Tooltip("Статус препятствия")]
    [SerializeField] private bool _status = true;
    [Tooltip("Объект препятствия")]
    [SerializeField] private GameObject _object;
    [Tooltip("Тип препятствия")]
    [SerializeField] private ObstacleType _type;
    [Tooltip("Максимальное отклонение")]
    [SerializeField] private float _maxDeviation;
    [Tooltip("Минимальное отклонение")]
    [SerializeField] private float _minDeviation;
    [Tooltip("Время действия эффекта")]
    [SerializeField] private float _duration;
    [Tooltip("Материал препятствия")]
    [SerializeField] private Material _material;
    [Tooltip("Материал препятствия 1")]
    [SerializeField] private Material _material1;
    [Tooltip("Материал препятствия 2")]
    [SerializeField] private Material _material2;
    [Tooltip("Ось вращения")]
    [SerializeField] private Vector3 _rotationAxes;

    private float _time = 0;
        
    private void Update()
    {
        _time += Time.deltaTime;
        switch (_type)
        {
            case ObstacleType.Disappear:
                SetDisappear(_time);
                break;
            case ObstacleType.Rotation:
                SetRotation(_time);
                break;
            case ObstacleType.Moving:
                SetMoving();
                break;

            default:
                break;
        }
    }

    private void SetDisappear(float time)
    {
        float interpolant = Mathf.InverseLerp(0, _duration, time);
        _material.color = Color.Lerp(_material1.color, _material2.color, interpolant);

        if (_time >= _duration)
        {
            _time = 0;
            _status = !_status;
        }
       
        _object.SetActive(_status);
    }

    private void SetMoving()
    {
        throw new NotImplementedException();
    }

    private void SetRotation(float time)
    {
        float interpolant = Mathf.InverseLerp(_minDeviation, _maxDeviation, time);
        _material.color = Color.Lerp(_material1.color, _material2.color, time);
        transform.Rotate(_rotationAxes, interpolant);

        if (_time >= _duration)
        {
            _time = 0;
            _status = !_status;
        }
    }

    public bool GetStatus()
    {
        return _status;
    }
    public ObstacleType GetObstacleType()
    {
        return _type;
    }
    public void SetStatus(bool status)
    {
        _status = status;
    }

} 
