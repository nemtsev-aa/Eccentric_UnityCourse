using UnityEngine;

public class TakeDamageOnTrigger : MonoBehaviour
{
    [Tooltip("¬раг получающий урон")]
    [SerializeField] private EnemyHealth _enemyHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
        {
            if (other.attachedRigidbody.GetComponent<Bullet>())
            {
                // ќцениваем эффект попадани€ дл€ здоровь€ противника
                _enemyHealth.TakeDamage(1);
                // ”ничтожаем пулю, попавшую в противника
                Destroy(other.gameObject);
            }
        }
    }
}
