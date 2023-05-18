using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [Tooltip("���������")]
    [SerializeField] private Collider _collider;
    [Tooltip("������ �� ������")]
    [SerializeField] private GameObject _explosionEffect;
    [Tooltip("���� ��� ��������������")]
    [SerializeField] private LayerMask _layerMask;
 

    private float _damage;
    private float _radius;

    public void Init(float damage, float radius)
    {
        _damage = damage;
        _radius = radius;
        Destroy(gameObject, 4f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyAnimal>())
            Explode();
    }

    private void Explode()
    {
        Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _layerMask);
        foreach (var collider in colliders)
        {
            collider.attachedRigidbody.GetComponent<EnemyHealth>().TakeDamage(_damage);
        }
        Destroy(gameObject, 0.5f);
    }
}
