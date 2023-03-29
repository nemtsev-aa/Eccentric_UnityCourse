using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("Время жизни пули")]
    [SerializeField] private float _lifeTime = 3f;
    [Tooltip("Эффект попадания")]
    [SerializeField] private GameObject _hitParticle;

    public event Action<GameObject> HitRegistered;
    private int _ricochet;

    private void Start()
    {
        // Подписываем счётчик попаданий на событие - попадание
        HitRegistered += HitCounter.Instance.HitCounting;

        // Уничтожаем пулю через определённое время, если она не достигла цели
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.attachedRigidbody && other.attachedRigidbody.GetComponent<EnemyHealth>())
            Hit(other.gameObject);
        // Активируем эффект ключей при попадании в них пули
        else if (other.TryGetComponent(out Key key))
        {
            GameObject obstacle = key.GetTarget();

            if (obstacle.TryGetComponent(out LimitRotation limitRotation))
            {
                limitRotation.SetStatus(true);
            }
            else if (obstacle.TryGetComponent(out DownToTopMoving downToTopMoving))
            {
                downToTopMoving.enabled = true;
            }
            Hit(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealth>())
            Hit(collision.gameObject);
        else
            Ricochet();
    }
    public virtual void Hit(GameObject collisionGameObject)
    {
        _ricochet = 0;
        HitRegistered?.Invoke(collisionGameObject);
        // Визуализируем попадание и уничтожаем пулю
        Instantiate(_hitParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public virtual void Ricochet()
    {
        _ricochet += 1;
        Debug.Log("Ricochet x" + _ricochet);
    }
}
