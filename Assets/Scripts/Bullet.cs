using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("����� ����� ����")]
    [SerializeField] private float _lifeTime = 3f;
    [Tooltip("������ ���������")]
    [SerializeField] private GameObject _hitParticle;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        CreateHitEffect();
        if (other.TryGetComponent<Key>(out Key key))
        {
            GameObject obstacle = key.GetTarget();

            if (obstacle.TryGetComponent<LimitRotation>(out LimitRotation limitRotation))
            {
                limitRotation.SetStatus(true);
            }
            else if (obstacle.TryGetComponent<LimitMoving>(out LimitMoving limitMoving))
            {
                limitMoving.SetStatus(true);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            CreateHitEffect();
            Destroy(gameObject);
        } 
    }

    private void CreateHitEffect()
    {
        Instantiate(_hitParticle, transform.position, transform.rotation);
    }
}
