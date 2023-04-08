using UnityEngine;

public class TakeDamageOnTrigger : MonoBehaviour
{
    [Tooltip("Враг получающий урон")]
    [SerializeField] private EnemyHealth _enemyHealth;
    [Tooltip("Погибать при любом столкновении")]
    [SerializeField] private bool DieOnAnyCollision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
        {
            Bullet bullet = other.attachedRigidbody.GetComponent<Bullet>();
            if (bullet)
            {
                // Оцениваем эффект попадания для здоровья противника
                _enemyHealth.TakeDamage(bullet.DamageValue);
                // Уничтожаем пулю, попавшую в противника
                Destroy(other.gameObject);
            }

            PlayerHealth playerHealth = other.attachedRigidbody.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                // Оцениваем эффект столкновения противника с игроком для здоровья противника
                _enemyHealth.TakeDamage(1);
            }
        }

        if (DieOnAnyCollision)
        {
            if (!other.isTrigger)
            {
                _enemyHealth.Die();
            } 
        }
    }
}
