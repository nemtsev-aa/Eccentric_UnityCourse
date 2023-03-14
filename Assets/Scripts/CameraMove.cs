using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Tooltip("����")]
    [SerializeField] private Transform _target;
    [Tooltip("�������� �������� ������ � ����")]
    [SerializeField] private float _lerpSpeed;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * _lerpSpeed);
    }
}
