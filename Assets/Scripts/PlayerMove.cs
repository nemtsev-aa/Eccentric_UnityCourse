using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private Transform _cameraCenter;
    [SerializeField] private float _torqueValue;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.maxAngularVelocity = 300f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody.AddTorque(_cameraCenter.right * Input.GetAxis("Vertical") * _torqueValue);
        _rigidbody.AddTorque(-_cameraCenter.forward * Input.GetAxis("Horizontal") * _torqueValue);
    }
}
