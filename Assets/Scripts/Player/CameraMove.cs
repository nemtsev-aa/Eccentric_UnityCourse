using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Rigidbody _playerRigidbody;

    private List<Vector3> _velocityList = new List<Vector3>();

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            _velocityList.Add(Vector3.zero);

        }
    }
    private void FixedUpdate()
    {
        _velocityList.Add(_playerRigidbody.velocity);
        _velocityList.RemoveAt(0);
    }
    private void Update()
    {
        Vector3 summ = Vector3.zero;

        for (int i = 0; i < _velocityList.Count; i++)
        {
            summ += _velocityList[i];
        }

        transform.position = _playerTransform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(summ), Time.deltaTime * 10f);
    }
}
