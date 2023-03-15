using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _health = 1;
    [SerializeField] private AudioSource _meHit;

    [SerializeField] private UnityEvent EventOnTakeDamage;
    public void TakeDamage(int danageValue)
    {
        EventOnTakeDamage.Invoke();
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
