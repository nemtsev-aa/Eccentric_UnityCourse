using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum EnemyType
{
    Rocket,
    Carrot,
    Acorn,
    Hen,
    Rabbit,
    Pig,
    Squirrel,
    Bear
}

public class EnemyHealth : MonoBehaviour
{
    [Tooltip("��� ����������")]
    [SerializeField] public EnemyType EnemyType;
    [Tooltip("������������ �������� ����������")]
    [SerializeField] private int _maxHealth = 1;
    [Tooltip("�������� ������ ����������")]
    [SerializeField] private AudioSource _meHit;
    [Tooltip("��������� �������� ����������")]
    [SerializeField] private Slider _healthView;
    [Tooltip("������ ������������")]
    public bool Invulnerable;       
    [Tooltip("������� - ��������� ����� �����������")]
    [SerializeField] private UnityEvent EventOnTakeDamage;
    [Tooltip("������ ������")]
    [SerializeField] private ParticleSystem _dieParticleEffect;
    
    // ������� - �������� �����
    public event Action<EnemyHealth> EnemyKilled;

    // ������� �������� ����������
    private int _health;
    // ���������
    private Enemies _enemies;


    private void Start()
    {
        _health = _maxHealth;
        _enemies = FindObjectOfType<Enemies>();
        EnemyKilled += _enemies.RemoveEnemy;
    }

    public void TakeDamage(int damageValue)
    {
        if (!Invulnerable)
        {
            EventOnTakeDamage.Invoke();
            
            _health -= damageValue;
            _meHit.Play();

            if (_healthView != null)
                ShowHealth();

            if (_health <= 0)
                Die();

            Invulnerable = true;
            // ����� ������������ �����������
            Invoke(nameof(StopInvulnerable), 1f); 
        }
    }

    public void StopInvulnerable()
    {
        Invulnerable = false;
    }

    public void Die()
    {
        EnemyHealth killedEnemy = gameObject.GetComponent<EnemyHealth>();
        EnemyKilled?.Invoke(killedEnemy);

        if (_dieParticleEffect != null)
        {
            ParticleSystem dieParticle = Instantiate(_dieParticleEffect, transform.position - Vector3.down * 1.5f, transform.rotation);
        }

        Destroy(gameObject);
    }

    private void ShowHealth()
    {
        float viewHealthValue = ((_health * 100) / _maxHealth) * 0.01f;
        _healthView.value = viewHealthValue;
    }

    public bool GetInvulnerable()
    {
        return Invulnerable;
    }

}
