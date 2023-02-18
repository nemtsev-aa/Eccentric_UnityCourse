using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Tooltip("�������� ��������")]
    [SerializeField] private float _rotationSpeed;
    [Tooltip("��������� ������������� �����������")]
    [SerializeField] private float _upDownAmplitude;

    void Update()
    {

        transform.position = new Vector3(transform.localPosition.x, _upDownAmplitude*2 + Mathf.Sin(Time.time) * _upDownAmplitude, transform.localPosition.z);
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }
}
