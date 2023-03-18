using UnityEngine;

public class TakeDamageOnTrigger : MonoBehaviour
{
    [SerializeField] EnemyHealth _enemyHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
        {
            if (other.attachedRigidbody.GetComponent<PlayerHealth>())
            {
                _enemyHealth.TakeDamage(1);
            }

            if (other.attachedRigidbody.GetComponent<Bullet>())
            {
                HitCounter.Instance.HitCounting(1);
                _enemyHealth.TakeDamage(1);
                Destroy(other.gameObject);
            }

        }
    }
}
