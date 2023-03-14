using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private AudioSource _meHit;
    public void TakeDamage(int danageValue)
    {
        _health -= danageValue;
        _meHit.Play();
        if (_health <= 0) 
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
