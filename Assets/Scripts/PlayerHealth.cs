using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Tooltip("������� ���������� �������� ���������")]
    [field: SerializeField] public int Health { get; private set; }
    [Tooltip("������������ ���������� �������� ���������")]
    [field: SerializeField] public int MaxHealth { get; private set; }
    [Tooltip("������ ������������")]
    [SerializeField] private bool _invulnerable;
    [Tooltip("��������� �������� ���������")]
    [SerializeField] private Image _healthView;
    [Tooltip("�������� �������� ���������")]
    [SerializeField] private Gradient _healthGradient;
    [Tooltip("������� - ��������� �����")]
    [SerializeField] private UnityEvent EventOnTakeDamage;
    [Tooltip("������� - �������")]
    [SerializeField] private UnityEvent EventOnAddHealth;

    private void Start()
    {
        ShowHealth();
    }

    public void TakeDamage(int danageValue)
    {
        if (!_invulnerable)
        {
            Health -= danageValue;
             ShowHealth();
            if (Health <= 0)
            {
                Health = 0;
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
        Health += healthValue;
        
        ShowHealth();

        if (Health > MaxHealth)
            Health = MaxHealth;
        EventOnAddHealth.Invoke();
    }

    private void Die()
    {
        //GameProcessManager.Instance.GameLose();
        Debug.Log("Die");
    }

    private void ShowHealth()
    {
        float viewHealthValue = ((Health * 100) / MaxHealth) * 0.01f;
        _healthView.fillAmount= viewHealthValue;
        _healthView.color = _healthGradient.Evaluate(viewHealthValue);
    }

    public void ResetHealth()
    {
        Health = 5;
        ShowHealth();
    }
}
