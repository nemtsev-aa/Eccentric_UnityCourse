using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("Время жизни пули")]
    [SerializeField] private float _lifeTime = 3f;
    [Tooltip("Эффект попадания")]
    [SerializeField] private GameObject _hitParticle;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
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

        Instantiate(_hitParticle, other.transform.position, other.transform.rotation);
    }
}
