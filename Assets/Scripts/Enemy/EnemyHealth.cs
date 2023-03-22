using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Tooltip("Максимальное здоровье противника")]
    [SerializeField] private int _maxHealth = 1;
    [Tooltip("Источник звуков противника")]
    [SerializeField] private AudioSource _meHit;
    [Tooltip("Индикатор здоровья противника")]
    [SerializeField] private Slider _healthView;
    [Tooltip("Событие - получение урона противником")]
    [SerializeField] private UnityEvent EventOnTakeDamage;
    // Текущее здоровье противника
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
    public void Die()
    {
        Destroy(gameObject);
    }
    private void ShowHealth()
    {
        float viewHealthValue = ((_health * 100) / _maxHealth) * 0.01f;
        _healthView.value = viewHealthValue;
    }
}
