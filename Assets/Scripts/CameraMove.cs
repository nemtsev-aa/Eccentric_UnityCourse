using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Tooltip("Тип ввода")]
    [SerializeField] private Transform _target;

    private void LateUpdate()
    {
        transform.position = _target.position;
    }
}
