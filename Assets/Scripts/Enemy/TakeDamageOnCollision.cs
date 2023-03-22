using UnityEngine;

public class TakeDamageOnCollision : MonoBehaviour
{
    [Tooltip("Враг получающий урон")]
    [SerializeField] private EnemyHealth _enemyHealth;
    [Tooltip("Погибать при любом столкновении")]
    [SerializeField] private bool DieOnAnyCollision;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody)
        {
            if (collision.rigidbody.GetComponent<Bullet>())
            {
                // Оцениваем эффект попадания для здоровья противника
                _enemyHealth.TakeDamage(1);
            }
        }

        if (DieOnAnyCollision)
        {
            _enemyHealth.Die();
        }
    }
}
