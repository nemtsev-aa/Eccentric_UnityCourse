using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _player;

    [SerializeField] private CoinCreator _coinCreator;

    private Rigidbody _playerRigidbody;
    private void Start()
    {
        _playerRigidbody = _player.gameObject.GetComponent<Rigidbody>();
        FindTarget();
    }
    private void Update()
    {
        MoveToPlayer();
        RotationToTarget();
    }

    private void MoveToPlayer()
    {
        transform.position = _player.position;
    }
    private void RotationToTarget()
    {
        FindTarget();

        if (_target == null)
        {
            transform.rotation = Quaternion.LookRotation(_player.forward);
        }
        else
        {
            Vector3 toTarget = _target.position - transform.position;
            Vector3 toTargetXZ = new Vector3(toTarget.x, 0f, toTarget.z);

            transform.rotation = Quaternion.LookRotation(toTargetXZ);
        }
    }

    private void FindTarget()
    {
        _target = _coinCreator.GetNearCoin(_player);
    }
}
