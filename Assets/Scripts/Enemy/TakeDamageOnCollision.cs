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
            Bullet bullet = collision.rigidbody.GetComponent<Bullet>();
            if (bullet)
                // Оцениваем эффект попадания для здоровья противника
                _enemyHealth.TakeDamage(bullet.DamageValue);
        }

        if (DieOnAnyCollision)
            _enemyHealth.Die();
    }
}
