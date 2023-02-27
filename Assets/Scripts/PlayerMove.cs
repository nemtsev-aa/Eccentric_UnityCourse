using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 5f;
    

    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * _rotationSpeed); 
    }
}
