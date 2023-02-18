using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _player;

    private void Update()
    {
        Vector3 toTarget = _target.position - transform.position;
        Vector3 toTargetXZ = new Vector3(toTarget.x, 0f, toTarget.z);

        transform.rotation = Quaternion.LookRotation(toTargetXZ);
        transform.position = _player.position;

    }

}
