using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health = 5;
    [SerializeField] private int _maxHealth = 8;
    [SerializeField] bool _invulnerable;
    [SerializeField] private Slider _healthView;

    [SerializeField] private UnityEvent EventOnTakeDamage;
    [SerializeField] private UnityEvent EventOnAddHealth;

    private void Start()
    {
        ShowHealth();
    }

    public void TakeDamage(int danageValue)
    {
        if (!_invulnerable)
        {
            _health -= danageValue;
             ShowHealth();
            if (_health <= 0)
            {
                _health = 0;
                Die();
            }
            _invulnerable = true;
            Invoke(nameof(StopInvulnerable), 1f);
            
            EventOnTakeDamage.Invoke();
        } 
    }

    private void StopInvulnerable()
    {
        _invulnerable = false;
    }

    public void AddHealth(int healthValue)
    {
        _health += healthValue;
        
        ShowHealth();

        if (_health > _maxHealth)
        {
            _health = _maxHealth;           
        }
        EventOnAddHealth.Invoke();
    }

    private void Die()
    {
        Debug.Log("You lose!");
    }

    private void ShowHealth()
    {
        float viewHealthValue = ((_health * 100) / _maxHealth) * 0.01f;
        _healthView.value = viewHealthValue;
    }
}
