using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public float CollectionDistance { get; internal set; }
    [field: SerializeField] public float Damage { get; internal set; }
    [field: SerializeField] public float MoveSpeed { get; internal set; }
    [field: SerializeField] public float ShotPeriod { get; internal set; }
    [field: SerializeField] public float Colldown { get; internal set; }

    //[Space(10)]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Collector _collector;
    [SerializeField] private RigidbodyMove _rigidbodyMove;
    
    public void UpdateProperties()
    {
        _collector.CollectionDistanceUpdate(CollectionDistance);
        _rigidbodyMove.SpeedUpdate(MoveSpeed);
    }
}
