using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Tooltip("Текущее количество здоровья персонажа")]
    [field: SerializeField] public float Health { get; private set; }
    [Tooltip("Максимальное количество здоровья персонажа")]
    [field: SerializeField] public float MaxHealth { get; private set; }
    [Tooltip("Статус неуязвимости")]
    [SerializeField] private bool _invulnerable;
    [Tooltip("Событие - получение урона")]
    public event Action<float, float> OnHealthChange;
    [Tooltip("Событие - лечение")]
    public event Action<float, float> OnAddHealth;
    [Tooltip("Событие - смерть персонажа")]
    public event Action OnDie;
    [SerializeField] private GameStateManager _gameStateManager;


    private void Start()
    {
        OnHealthChange?.Invoke(Health, MaxHealth);
    }

    public void TakeDamage(float danageValue)
    {
        if (!_invulnerable)
        {
            Health -= danageValue;
            Health = Mathf.Max(Health, 0);
            OnHealthChange?.Invoke(Health, MaxHealth);

            if (Health == 0) Die();

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
        Health += healthValue;
        Health = Mathf.Min(Health, MaxHealth);

        OnAddHealth?.Invoke(Health, MaxHealth);
    }

    private void Die()
    {
        //GameProcessManager.Instance.GameLose();
        OnDie?.Invoke();
        _gameStateManager.SetLose();
        Debug.Log("Die");
    }

    public void ResetHealth()
    {
        Health = 5;
        OnHealthChange?.Invoke(Health, MaxHealth);
    }
}
