using UnityEngine;

public class TakeDamageOnCollision : MonoBehaviour
{
    public EnemyHealth EnemyHealth;
    [SerializeField] private bool DieOnAnyCollision;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody)
        {
            if (collision.rigidbody.GetComponent<Bullet>())
            {
                HitCounter.Instance.HitCounting(1);
                EnemyHealth.TakeDamage(1);
            }
        }

        if (DieOnAnyCollision)
        {
            if (!collision.rigidbody || !collision.rigidbody.GetComponent<EnemyHealth>())
            {
                EnemyHealth.TakeDamage(1000);
            }
        }
    }
}
