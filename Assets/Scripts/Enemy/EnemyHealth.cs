using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 1;
    [SerializeField] private AudioSource _meHit;
    [SerializeField] private Slider _healthView;
    [SerializeField] private UnityEvent EventOnTakeDamage;
     
    private int _health;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damageValue)
    {
        EventOnTakeDamage.Invoke();
        _health -= damageValue;
        _meHit.Play();
        
        if (_healthView != null)
        {
            ShowHealth();
        }
        
        if (_health <= 0) 
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private void ShowHealth()
    {
        float viewHealthValue = ((_health * 100) / _maxHealth) * 0.01f;
        _healthView.value = viewHealthValue;
    }
}
