using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("Время жизни пули")]
    [SerializeField] private float _lifeTime = 3f;
    [Tooltip("Эффект попадания")]
    [SerializeField] private GameObject _hitParticle;

    public event Action HitRegistered;

    private void Start()
    {
        // Подписываем счётчик попаданий на событие - попадание
        HitRegistered += HitCounter.Instance.HitCounting;

        // Уничтожаем пулю через определённое время, если она не достигла цели
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Активируем эффект ключей при попадании в них пули
        if (other.TryGetComponent<Key>(out Key key))
        {
            GameObject obstacle = key.GetTarget();

            if (obstacle.TryGetComponent(out LimitRotation limitRotation))
            {
                limitRotation.SetStatus(true);
            }
            else if (obstacle.TryGetComponent(out LimitMoving limitMoving))
            {
                limitMoving.SetStatus(true);
            }
        }
        Hit();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            Hit();
        } 
    }
    public void Hit()
    {
        HitRegistered?.Invoke();
        // Визуализируем попадание и уничтожаем пулю
        Instantiate(_hitParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
