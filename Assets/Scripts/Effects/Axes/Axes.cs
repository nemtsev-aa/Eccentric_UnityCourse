using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axes : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _damage;

    private Transform _target;
    private float _damageBoost;

    public void Setup(Transform target, float damageBoost)
    {
        _target = target;
    }

    void Update()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
        transform.position = _target.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyAnimal enemy))
        {
            other.attachedRigidbody.GetComponent<EnemyHealth>().TakeDamage(_damage + _damageBoost);
        }
    }
}
