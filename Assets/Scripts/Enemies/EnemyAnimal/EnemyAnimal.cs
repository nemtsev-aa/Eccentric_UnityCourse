#if UNITY_EDITOR
using UnityEditor;
#endif  

using System;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyAnimalType
{
    Hen,
    Rabbit,
    Pig,
    Squirrel,
    Bear,
    Spike,
    Shell,
    HermitKing
}

public class EnemyAnimal : Enemy
{
    [Header("Animal Settings")]
    // Текущий статус активации объекта
    public bool IsActive = true;
    [Tooltip("Тип противника")]
    [SerializeField] public EnemyAnimalType EnemyAnimalType;
    [Tooltip("Индикатор здоровья противника")]
    [SerializeField] private Slider _healthView;
    [Tooltip("Эффект смерти")]
    [SerializeField] private ParticleSystem _dieParticleEffect;
    [Tooltip("Звук получения урона")]
    [SerializeField] private AudioClip _meHit;
    [Tooltip("Звук смерти")]
    [SerializeField] private AudioClip _meDie;

    [Tooltip("Событие - враг уничтожен")]
    public event Action<EnemyAnimal> EnemyKilled;
    //Источник звука
    private AudioSource _audioSource;
    // Эффект получения урона
    private BlinkEffect _blinkEffect;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _blinkEffect = GetComponent<BlinkEffect>();
    }

    private void OnEnable()
    {
        EnemyHealth.HealthDecreased += ShowHealth;
        EnemyHealth.HealthIsOver += Die;
    }

    private void OnDisable()
    {
        EnemyHealth.HealthDecreased -= ShowHealth;
        EnemyHealth.HealthIsOver -= Die;
    }

    private void Die(EnemyHealth enemyHealth)
    {
        // Создаём эффект смери
        ParticleSystem dieParticle = Instantiate(_dieParticleEffect, enemyHealth.transform.position - Vector3.down * 1.5f, enemyHealth.transform.rotation);
        // Сообщаем подписчикам об убийстве врага
        EnemyKilled?.Invoke(this);
        // Проигрываем звук смерти
        PlayDieSound();
        // Удаляем объект врага со сцены
        Destroy(enemyHealth.gameObject);
        // Создаём префаб опыта на месте смерти врага
        GameObject experienceLoot = Instantiate(_experienceLoot, transform.position, transform.rotation);
    }

    private void ShowHealth(int currentValue, int maxValue)
    {
        if (_blinkEffect != null)
            _blinkEffect.StartBlink();
        
        // Отображаем здоровье с помощью слайдера
        float viewHealthValue = ((currentValue * 100) / maxValue) * 0.01f;
        _healthView.value = viewHealthValue;
        // Проигрываем звук получения урона
        PlayHitSound();
    }

    private void PlayHitSound()
    {
        _audioSource.clip = _meHit;
        _audioSource.Play();
    }

    private void PlayDieSound()
    {
        _audioSource.clip = _meDie;
        _audioSource.Play();
    }
}

