using System.Collections;
using System.Collections.Generic;
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
