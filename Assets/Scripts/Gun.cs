using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Tooltip("Префаб пули")]
    [SerializeField] private GameObject _bulletPrafab;
    [Tooltip("Расположение генератора пуль")]
    [SerializeField] private Transform _bulletCreator;
    [Tooltip("Скорость пули")]
    [SerializeField] private float _bulletSpeed = 10f;
    [Tooltip("Скорострельность")]
    [SerializeField] private float _shotPeriod = 0.2f;

    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _shotPeriod)
        {
            if ((Input.GetMouseButton(0)))
            {
                _timer = 0;
                GameObject newBullet = Instantiate(_bulletPrafab, _bulletCreator.position, _bulletCreator.rotation);
                newBullet.GetComponent<Rigidbody>().velocity = _bulletCreator.forward * _bulletSpeed;
            }
        }     
    }
} 
