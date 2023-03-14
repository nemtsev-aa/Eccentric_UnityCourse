using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health = 5;
    [SerializeField] private int _maxHealth = 8;
    [SerializeField] bool _invulnerable;
    [SerializeField] private AudioSource _pickUpHealth;
    [SerializeField] private AudioSource _meHit;
    [SerializeField] private Slider _healthView;

    private void Start()
    {
        ShowHealth();
    }

    public void TakeDamage(int danageValue)
    {
        if (!_invulnerable)
        {
            _health -= danageValue;
            _meHit.Play();
            ShowHealth();
            if (_health <= 0)
            {
                _health = 0;
                Die();
            }
            _invulnerable = true;
            Invoke(nameof(StopInvulnerable), 1f);
        } 
    }

    private void StopInvulnerable()
    {
        _invulnerable = false;
    }

    public void AddHealth(int healthValue)
    {
        _health += healthValue;
        _pickUpHealth.Play();
        ShowHealth();
        if (_health > _maxHealth)
        {
            _health = _maxHealth;           
        }
    }

    private void Die()
    {
        Debug.Log("You lose!");
    }

    private void ShowHealth()
    {
        float viewHealthValue = ((_health * 100) / _maxHealth) * 0.01f;
        Debug.Log(viewHealthValue);
        _healthView.value = viewHealthValue;
    }
   
}
