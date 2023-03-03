using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyRotation : MonoBehaviour
{
    [Tooltip("Цель")]
    [SerializeField] private Transform _target;
    [Tooltip("Угол поворота тела вокруг оси Y")]
    [SerializeField] private float bodyYRotation = 30f;
    [Tooltip("Скорость поворота к цели")]
    [SerializeField] private float _lerpSpeed;

    void Update()
    {
        // Поворот относительно Y
        Quaternion yQuaternion = Quaternion.AngleAxis(_target.position.x > 0 ? -bodyYRotation : bodyYRotation, Vector3.up);
        // Тернарный оператор
        transform.rotation = Quaternion.Lerp(transform.rotation, yQuaternion, Time.deltaTime * _lerpSpeed);

        // Подробная запись
        //if (_target.position.x > 0)
        //{
        //    bodyYRotation = -bodyYRotation;
        //}
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(bodyYRotation, Vector3.up), Time.deltaTime * _lerpSpeed);

    }
}
