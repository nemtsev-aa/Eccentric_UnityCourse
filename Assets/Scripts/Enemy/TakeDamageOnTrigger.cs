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
            if (other.attachedRigidbody.GetComponent<Bullet>())
            {
                // Оцениваем эффект попадания для здоровья противника
                _enemyHealth.TakeDamage(1);
                // Уничтожаем пулю, попавшую в противника
                Destroy(other.gameObject);
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
