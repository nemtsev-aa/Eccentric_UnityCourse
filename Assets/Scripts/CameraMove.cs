using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Tooltip("Цель")]
    [SerializeField] private Transform _target;
    [Tooltip("Скорость поворота камеры к цели")]
    [SerializeField] private float _lerpSpeed;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * _lerpSpeed);
    }
}
