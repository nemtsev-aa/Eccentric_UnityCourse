using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType
{
    Disappear,
    Rotation,
    LimitRotation,
    Moving,
    LimitMoving
}

public class Disappier : MonoBehaviour
{
    [Header("Obstacle Settings")]
    [Tooltip("��� �����������")]
    [SerializeField] private ObstacleType _type;
    [Tooltip("������ �����������")]
    [SerializeField] private bool _status = true;
    [Tooltip("������ �����������")]
    [SerializeField] private GameObject _object;
    [Tooltip("����� �������� �������")]
    [SerializeField] private float _duration;
    [Header("Materials")]
    [Tooltip("�������� �����������")]
    [SerializeField] private Material _material;
    [Tooltip("�������� ����������� 1")]
    [SerializeField] private Material _material1;
    [Tooltip("�������� ����������� 2")]
    [SerializeField] private Material _material2;

    private float _time = 0;

    private void Start()
    {
        _type = ObstacleType.Disappear;
    }
    private void Update()
    {
        _time += Time.deltaTime;

        SetDisappear();
        SetColor(_time);

        if (_time >= _duration)
        {
            _time = 0;
            _status = !_status;
        }
    }

    private void SetDisappear()
    {
        _object.SetActive(_status);
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
